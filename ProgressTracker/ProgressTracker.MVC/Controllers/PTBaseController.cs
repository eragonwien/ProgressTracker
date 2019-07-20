using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProgressTracker.MVC.Controllers
{
   public class PTBaseController : Controller
   {
      public int UserId { get { return GetUserId(); } }

      public PTBaseController()
      {

      }

      private int GetUserId()
      {
         return User != null && User.Identity.IsAuthenticated && int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) ? userId : -1;
      }
   }
}