﻿using System;
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
      public int Id { get; set; }
      public string Title { get; set; }
      public string ReturnUrl { get; set; }
      public string EditUrl { get; set; }
      public string DeleteUrl { get; set; }
      public bool IsAuthenticated { get; set; }

      public TopNavbarViewModel(string title, bool isAuthenticated = false, int id = 0, string returnUrl = null, string editUrl = null, string deleteUrl = null)
      {
         Id = id;
         Title = title;
         ReturnUrl = returnUrl;
         EditUrl = editUrl;
         DeleteUrl = deleteUrl;
         IsAuthenticated = isAuthenticated;
      }
   }

   public class ProjectViewModel
   {
      public ProjectViewModel(Ptproject project)
      {
         Project = project;
         SetProgress(project);
      }

      public Ptproject Project { get; set; }
      public decimal Progress { get; set; }
      public int UnresolvedCount { get; set; }

      private void SetProgress(Ptproject project)
      {
         decimal progress = -1;
         try
         {
            int total = project.Pttask.Count;
            int completed = project.Pttask.Count(o => o.Completed);
            progress = total != 0 ? completed / total : 0;
            Progress = progress >= 0 && progress <= 1 ? progress : Progress;
            UnresolvedCount = total - completed;
         }
         catch (Exception ex)
         {
            throw ex;
         }

      }
   }
}
