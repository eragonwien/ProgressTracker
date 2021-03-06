﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using ProgressTracker.Models;
using ProgressTracker.Services;
using SNGCommon.Resources;

namespace ProgressTracker.Pages
{
   public class ProjectModel : BasePageModel
   {
      private readonly IProjectService projectService;
      private readonly IStringLocalizer<Translation> localizer;

      [BindProperty(SupportsGet = true)]
      public int Id { get; set; }

      [BindProperty]
      public Ptproject Project { get; set; }

      public ProjectModel(IProjectService projectService, IStringLocalizer<Translation> localizer)
      {
         this.projectService = projectService;
         this.localizer = localizer;
      }

      public async Task OnGetAsync()
      {
         Project = await projectService.GetOne(Id);
         Project.Pttask = Project.Pttask.OrderBy(t => t.Completed).ToList();
      }

      public void OnGetAddAsync()
      {
         Project = new Ptproject() { PtuserId = UserId };
      }

      public async Task<IActionResult> OnPostAddAsync()
      {
         try
         {
            projectService.Create(Project);
            await projectService.SaveChanges();
            ActiveProjectId = Project.Id;
            Message = localizer[Translation.ProjectAdded, Project.Name];
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         return RedirectLocalUrl(ReturnUrl);
      }

      public async Task<IActionResult> OnPostEditAsync()
      {
         try
         {
            projectService.Patch(Project, nameof(Project.Name), nameof(Project.Description));
            await projectService.SaveChanges();
            ActiveProjectId = Project.Id;
            Message = localizer[Translation.ChangeOfProjectSaved, Project.Name];
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         ReturnUrl = Url.IsLocalUrl(ReturnUrl) ? ReturnUrl : "/";
         return RedirectLocalUrl(ReturnUrl);
      }

      public async Task<IActionResult> OnPostDeleteAsync()
      {
         try
         {
            projectService.Remove(Project.Id);
            await projectService.SaveChanges();
            ActiveProjectId = 0;
            Message = localizer[Translation.ProjectDeleted, Project.Name];
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         return Redirect("/");
      }

      public async Task<IActionResult> OnPostRestoreAsync()
      {
         try
         {
            projectService.Restore(Project.Id);
            await projectService.SaveChanges();
            ActiveProjectId = Project.Id;
            Message = localizer[Translation.ProjectRestored, Project.Name];
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         return Redirect("/");
      }
   }
}