using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProgressTracker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ProgressTracker.Pages
{
   public class SettingsModel : BasePageModel
   {
      private readonly IOptions<RequestLocalizationOptions> locOptions;
      [BindProperty]
      public CultureInfo Language { get; set; }
      public List<CultureInfo> SupportedLanguages { get; set; }

      public SettingsModel(IOptions<RequestLocalizationOptions> locOptions)
      {
         this.locOptions = locOptions;
         SupportedLanguages = locOptions.Value.SupportedCultures.ToList();
      }

      public void OnGet()
      {
         Language = Request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
      }

      public IActionResult OnPostLanguage()
      {
         Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName, 
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Language)), 
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
         return RedirectToPage();
      }
   }
}