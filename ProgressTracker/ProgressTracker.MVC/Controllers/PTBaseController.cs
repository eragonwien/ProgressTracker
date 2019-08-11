using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System.Security.Claims;

namespace ProgressTracker.MVC.Controllers
{
   public class PTBaseController : Controller
   {
      protected readonly ILogger<PTBaseController> log;
      public int UserId { get { return GetUserId(); } }

      public PTBaseController(ILogger<PTBaseController> log)
      {
         this.log = log;
      }

      private int GetUserId()
      {
         return User != null && User.Identity.IsAuthenticated && int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : -1;
      }

      public IActionResult RedirectLocal(string url)
      {
         return Url.IsLocalUrl(url) ? Redirect(url) : Redirect(Url.Content("~/"));
      }

      public void SetReturnUrl(string returnUrl = null)
      {
         ViewBag.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "/";
      }
   }
}