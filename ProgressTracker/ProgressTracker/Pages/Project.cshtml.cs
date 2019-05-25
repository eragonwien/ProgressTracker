using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.Models;
using ProgressTracker.Services;
using SNGCommon.Resources;

namespace ProgressTracker.Pages
{
   public class ProjectModel : BasePageModel
   {
      private readonly IProjectService projectService;

      [BindProperty(SupportsGet = true)]
      public int Id { get; set; }

      [BindProperty]
      public Ptproject Project { get; set; }
      public ProjectModel(IProjectService projectService)
      {
         this.projectService = projectService;
      }

      public void OnGetCreate()
      {
         IsCreate = true;
         ReturnUrl = "/";
      }

      public async Task<IActionResult> OnPostEditAsync()
      {
         try
         {
            projectService.Patch(Project, nameof(Project.Name), nameof(Project.Description));
            await projectService.SaveChanges();
            ActiveProjectId = Project.Id;
            Message = Translation.Completed;
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         ReturnUrl = Url.IsLocalUrl(ReturnUrl) ? ReturnUrl : "/";
         return Redirect(ReturnUrl);
      }
   }
}