using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.MVC.Models
{
   public class EmptyListViewModel
   {
      public string Title { get; set; }
      public string Subtitle { get; set; }
      public string ButtonText { get; set; }
      public string ButtonIcon { get; set; }
      public string ButtonLink { get; set; }

      public EmptyListViewModel(string title = null, string subtitle = null, string buttonText = null, string buttonIcon = null, string buttonLink = null)
      {
         Title = title;
         Subtitle = subtitle;
         ButtonText = buttonText;
         ButtonIcon = buttonIcon;
         ButtonLink = buttonLink;
      }
   }
}
