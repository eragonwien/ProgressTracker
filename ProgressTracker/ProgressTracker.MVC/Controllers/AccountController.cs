using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ProgressTracker.MVC.Models;
using ProgressTracker.MVC.Services;
using SNGCommon.Models;
using SNGCommon.Resources;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgressTracker.MVC.Controllers
{
   [Authorize]
   public class AccountController : PTBaseController
   {
      private readonly IUserService userService;
      private readonly IStringLocalizer<Translation> localizer;

      public AccountController(IUserService userService, IStringLocalizer<Translation> localizer, ILogger<PTBaseController> log) : base(log)
      {
         this.userService = userService;
         this.localizer = localizer;
      }

      [AllowAnonymous]
      [HttpGet]
      public IActionResult Login()
      {
         return View(new LoginViewModel { Language = Settings.Culture_EN });
      }

      [AllowAnonymous]
      [HttpPost]
      public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
      {
         if (!ModelState.IsValid)
         {
            return RedirectToAction();
         }
         AuthenticationResult result = await userService.AuthenticateAsync(loginViewModel.Email, loginViewModel.Password);
         if (result.Succeed)
         {
            await SignInAsync(result.AuthenticationEmail, result.AuthenticationName, result.AuthenticationId, loginViewModel.Language);
            return RedirectToAction("Index", "Project");
         }
         else
         {
            ModelState.AddModelError("", result.Message);
            return RedirectToAction();
         }
      }

      [HttpGet]
      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync();
         return RedirectToAction("Login");
      }

      private async Task SignInAsync(string email, string name, int userId, string language, AuthenticationProperties authProperties = null, string authScheme = CookieAuthenticationDefaults.AuthenticationScheme)
      {
         List<Claim> claims = new List<Claim>
         {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
         };
         var userIdentity = new ClaimsIdentity(claims, authScheme);
         var principal = new ClaimsPrincipal(userIdentity);

         authProperties = authProperties ?? new AuthenticationProperties();
         authProperties.IsPersistent = true;
         authProperties.ExpiresUtc = DateTime.Now.AddDays(Settings.CookieMaxAgeInDays);

         await HttpContext.SignOutAsync();
         await HttpContext.SignInAsync(principal, authProperties);

         Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
      }
   }
}