using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Models
{
   public class DetailNavbarViewModel
   {
      public string Title { get; set; }
      public string Subtitle { get; set; }

   }

   public class TopNavbarViewModel
   {
      public string Title { get; set; }
      public string ReturnUrl { get; set; }
      public string AddUrl { get; set; }
      public string EditUrl { get; set; }
      public string DeleteUrl { get; set; }
      public bool IsAuthenticated { get; set; }
      public Ptproject Project { get; set; }

      public TopNavbarViewModel(string title, bool isAuthenticated = false, string returnUrl = null, string addUrl = null, string editUrl = null, string deleteUrl = null, Ptproject project = null)
      {
         Title = title;
         ReturnUrl = returnUrl;
         AddUrl = addUrl;
         EditUrl = editUrl;
         DeleteUrl = deleteUrl;
         IsAuthenticated = isAuthenticated;
         Project = project;
      }
   }

   public class ProjectViewModel
   {
      public ProjectViewModel(Ptproject project)
      {
         Project = project;
         SetProgress(project);
         SetStatus();
      }

      private void SetStatus()
      {
         if (Progress == 0)
         {
            Status = ProjectStatus.Saved;
         }
         else if (Progress > 0 && Progress < 1)
         {
            Status = ProjectStatus.InProgress;
         }
         else if (Progress == 1)
         {
            Status = ProjectStatus.Completed;
         }
         else
         {
            Status = ProjectStatus.None;
         }
      }

      public Ptproject Project { get; set; }
      public decimal Progress { get; set; }
      public int UnresolvedCount { get; set; }
      public ProjectStatus Status { get; set; }

      private void SetProgress(Ptproject project)
      {
         decimal progress = -1;
         try
         {
            int total = project.Pttask.Count;
            int completed = project.Pttask.Count(o => o.Completed);
            progress = total != 0 ? (decimal)completed / total : 0;
            Progress = progress >= 0 && progress <= 1 ? progress : Progress;
            if (total == 0)
            {
               Progress = 0;
            }
            UnresolvedCount = total - completed;
         }
         catch (Exception ex)
         {
            throw ex;
         }

      }
   }
}
