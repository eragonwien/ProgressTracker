﻿using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgressTracker.Models
{
   public class BasePageModel : PageModel
   {
      private string returnUrl;
      [ViewData]
      [BindProperty(SupportsGet = true)]
      public string ReturnUrl
      {
         get { return Url.IsLocalUrl(returnUrl) ? returnUrl : string.Empty; }
         set { returnUrl = value; }
      }
      [ViewData]
      public bool IsCreate { get; set; }
      [TempData]
      public int ActiveProjectId { get; set; }
      [TempData]
      public string Message { get; set; }
      public TopNavbarViewModel NavbarModel { get; set; }

      public int UserId
      {
         get
         {
            return int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : -1;
         }
      }

      internal RedirectResult RedirectLocalUrl(string url)
      {
         return Url.IsLocalUrl(url) ? Redirect(url) : Redirect("/");
      }
   }
}
