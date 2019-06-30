using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProgressTracker.Models;
using ProgressTracker.Services;
using SNGCommon;

namespace ProgressTracker.Pages
{
   public class RegisterModel : BasePageModel
   {
      private readonly IUserService userService;
      private readonly ILogger<RegisterModel> log;

      [BindProperty]
      public Ptuser Ptuser { get; set; }

      public RegisterModel(IUserService userService, ILogger<RegisterModel> log)
      {
         this.userService = userService;
         this.log = log;
      }

      public void OnGet()
      {

      }

      public async Task<IActionResult> OnPostAsync()
      {
         if (ModelState.IsValid)
         {
            try
            {
               userService.Register(Ptuser);
               await userService.SaveChanges();
               return RedirectLocalUrl(ReturnUrl);
            }
            catch (Exception ex)
            {
               log.LogWarning(ex.Message);
               ModelState.AddModelError("", ex.Message);
            }
         }
         return Page();
      }
   }
}