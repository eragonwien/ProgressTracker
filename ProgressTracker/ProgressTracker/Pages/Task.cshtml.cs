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
   public class TaskModel : BasePageModel
   {
      private readonly ITaskService taskService;

      public TaskModel(ITaskService taskService)
      {
         this.taskService = taskService;
      }

      [BindProperty]
      public Pttask task { get; set; }

      [BindProperty(SupportsGet = true)]
      public int Id { get; set; }

      public async Task OnGetAsync()
      {
         IsCreate = false;
         task = await taskService.GetOne(Id);
         ReturnUrl = Request.Headers["Referer"].ToString();
      }

      public async Task<IActionResult> OnPostDescriptionAsync()
      {
         try
         {
            if (ModelState.IsValid)
            {
               taskService.Patch(task, nameof(task.Description));
               await taskService.SaveChanges();
               ActiveProjectId = task.PtprojectId;
            }
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
         return Redirect(ReturnUrl);
      }

      public async Task OnPostStatusAsync()
      {
         try
         {
            taskService.Patch(task, nameof(task.Completed));
            await taskService.SaveChanges();
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
      }

      public async Task OnPostDeleteAsync()
      {
         taskService.Remove(task.Id);
         await taskService.SaveChanges();
      }

      public async Task<IActionResult> OnPostAddAsync()
      {
         try
         {
            taskService.Create(task);
            await taskService.SaveChanges();
            ActiveProjectId = task.PtprojectId;
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