using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.Models;
using ProgressTracker.Services;
using SNGCommon.Resources;

namespace ProgressTracker.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService userService;

        [BindProperty]
        [Required(ErrorMessageResourceName = nameof(Translation.ValidationEmptyEmail), ErrorMessageResourceType = typeof(Translation))]
        [EmailAddress(ErrorMessageResourceName = nameof(Translation.ValidationInvalidEmail), ErrorMessageResourceType = typeof(Translation))]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessageResourceName = nameof(Translation.ValidationEmptyPassword), ErrorMessageResourceType = typeof(Translation))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Language { get; set; } = "en";

        public LoginModel(IUserService userService)
        {
            this.userService = userService;
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
                var user = await userService.Login(Email, Password);

                // Login erfolgreich
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(userIdentity);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(Settings.COOKIE_MAX_AGE_DAYS)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
    }
}