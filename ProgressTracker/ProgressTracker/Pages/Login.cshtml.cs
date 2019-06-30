using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ProgressTracker.Models;
using ProgressTracker.Services;
using SNGCommon.Resources;

namespace ProgressTracker.Pages
{
   public class LoginModel : BasePageModel
   {
      private readonly IUserService userService;
      private readonly ILogger<LoginModel> log;
      private readonly IStringLocalizer<Translation> localizer;

      [BindProperty]
      [Required(ErrorMessage = Translation.ValidationEmptyEmail)]
      [EmailAddress(ErrorMessage = Translation.ValidationInvalidEmail)]
      public string Email { get; set; }

      [BindProperty]
      [Required(ErrorMessage = Translation.ValidationEmptyPassword)]
      [DataType(DataType.Password)]
      public string Password { get; set; }
      public string Language { get; set; } = Settings.Culture_EN;

      public LoginModel(IUserService userService, ILogger<LoginModel> log, IStringLocalizer<Translation> localizer)
      {
         this.userService = userService;
         this.log = log;
         this.localizer = localizer;
      }

      public void OnGet()
      {

      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (!ModelState.IsValid)
         {
            return Page();
         }

         try
         {
            var user = await userService.Authenticate(Email, Password);
            await SignInAsync(user.Email, user.Name, user.Id);
            return Url.IsLocalUrl(ReturnUrl) ? Redirect(ReturnUrl) : Redirect("/");
         }
         catch (Exception ex)
         {
            ModelState.AddModelError("", ex.Message);
            return Page();
         }
      }

      public IActionResult OnGetExternal(string provider)
      {
         return Challenge(new AuthenticationProperties { RedirectUri = Url.Page("Login", "LoginRedirect") }, provider);
      }

      public async Task<IActionResult> OnGetLoginRedirectAsync()
      {
         if (!User.Identity.IsAuthenticated)
         {
            return Page();
         }

         string email = User.FindFirstValue(ClaimTypes.Email);
         if (!userService.Exists(email))
         {
            userService.Register(new Ptuser(email, User.FindFirstValue(ClaimTypes.Name), true), true);
            await userService.SaveChanges();
         }
         var user = await userService.GetOne(email);

         await SignInAsync(email, user.Name, user.Id);
         return RedirectLocalUrl(ReturnUrl);
      }

      private async Task SignInAsync(string email, string name, int userId, AuthenticationProperties authProperties = null, string authScheme = CookieAuthenticationDefaults.AuthenticationScheme)
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
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Language)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
      }
   }
}