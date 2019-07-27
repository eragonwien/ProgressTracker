using ProgressTracker.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.MVC.Services
{
   public static class Helper
   {
      public static string GetIconName(ProjectStatus status)
      {
         string name = string.Empty;
         switch (status)
         {
            case ProjectStatus.Saved:
               name = "assignment";
               break;
            case ProjectStatus.InProgress:
               name = "directions_run";
               break;
            case ProjectStatus.Completed:
               name = "assignment_turned_in";
               break;
            default:
               break;
         }
         return name;
      }

      public static string GetIconColour(ProjectStatus status)
      {
         string colour = string.Empty;
         switch (status)
         {
            case ProjectStatus.Saved:
               colour = "green";
               break;
            case ProjectStatus.InProgress:
               colour = "orange";
               break;
            case ProjectStatus.Completed:
               colour = "green";
               break;
            default:
               break;
         }
         return colour;
      }
   }
}
