using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.MVC.Models
{
   public class SettingListViewModel
   {
      public string Language { get; set; }
      public List<SelectListItem> AvailableLanguages { get; set; } = new List<SelectListItem>();
   }
}
