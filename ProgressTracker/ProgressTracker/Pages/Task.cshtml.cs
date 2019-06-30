using System;
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
   public class TaskModel : BasePageModel
   {
      private readonly ITaskService taskService;
      private readonly IProjectService projectService;
      private readonly IStringLocalizer<TaskModel> localizer;

      public TaskModel(ITaskService taskService, IProjectService projectService, IStringLocalizer<TaskModel> localizer)
      {
         this.taskService = taskService;
         this.projectService = projectService;
         this.localizer = localizer;
      }

      [BindProperty]
      public Pttask Task { get; set; }

      [BindProperty(SupportsGet = true)]
      public int Id { get; set; }

      public async Task OnGetAsync()
      {
         IsCreate = false;
         Task = await taskService.GetOne(Id);
         ReturnUrl = Request.Headers["Referer"].ToString();
      }

      public async Task<IActionResult> OnPostDescriptionAsync()
      {
         try
         {
            if (ModelState.IsValid)
            {
               taskService.Patch(Task, nameof(Task.Description));
               await taskService.SaveChanges();
               ActiveProjectId = Task.PtprojectId;
            }
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         return RedirectLocalUrl(ReturnUrl);
      }

      public async Task OnPostStatusAsync()
      {
         try
         {
            taskService.Patch(Task, nameof(Task.Completed));
            await taskService.SaveChanges();
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
      }

      public async Task OnPostDeleteAsync()
      {
         taskService.Remove(Task.Id);
         await taskService.SaveChanges();
      }

      public async Task<IActionResult> OnPostAddAsync()
      {
         try
         {
            taskService.Create(Task);
            await taskService.SaveChanges();
            ActiveProjectId = Task.PtprojectId;
            Message = localizer[Translation.Completed];
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         ReturnUrl = Url.IsLocalUrl(ReturnUrl) ? ReturnUrl : "/";
         return RedirectLocalUrl(ReturnUrl);
      }
   }
}