using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProgressTracker.MVC.Models;
using SNGCommon.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ProgressTracker.MVC.Controllers
{
   public class SettingController : PTBaseController
   {
      private readonly IStringLocalizer<Translation> localizer;
      private readonly IOptions<RequestLocalizationOptions> locOptions;
      private readonly List<CultureInfo> supportedCultures = new List<CultureInfo>();

      public SettingController(IStringLocalizer<Translation> localizer, IOptions<RequestLocalizationOptions> locOptions, ILogger<PTBaseController> log) : base(log)
      {
         this.localizer = localizer;
         this.locOptions = locOptions;
         supportedCultures = locOptions.Value.SupportedCultures.ToList();
      }

      [HttpGet]
      public IActionResult Index()
      {
         var model = new SettingListViewModel
         {
            Language = Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name,
            AvailableLanguages = supportedCultures.Select(c => new SelectListItem(c.NativeName, c.Name)).ToList()
         };
         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Edit(SettingListViewModel model)
      {
         if (ModelState.IsValid)
         {
            UpdateLanguage(model.Language);
         }
         return RedirectToAction(nameof(Index));
      }

      private void UpdateLanguage(string language)
      {
         if (supportedCultures.Any(l => l.Name == language))
         {
            CultureInfo languageInfo = supportedCultures.Single(c => c.Name == language);
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(languageInfo)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
         }
      }
   }
}