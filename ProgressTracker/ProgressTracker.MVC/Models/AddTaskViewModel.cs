using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.MVC.Models
{
   public class AddTaskViewModel
   {
      public string Description { get; set; }
      public int ProjectId { get; set; }
      public List<SelectListItem> ProjectIdList { get; set; } = new List<SelectListItem>();
   }
}
