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
   public class ObjectiveModel : BasePageModel
   {
      private readonly IObjectiveService objectiveService;

      public ObjectiveModel(IObjectiveService objectiveService)
      {
         this.objectiveService = objectiveService;
      }

      [BindProperty]
      public Ptobjective Objective { get; set; }

      [BindProperty(SupportsGet = true)]
      public int Id { get; set; }

      public async Task OnGetAsync()
      {
         IsCreate = false;
         Objective = await objectiveService.GetOne(Id);
         ReturnUrl = Request.Headers["Referer"].ToString();
      }

      public async Task<IActionResult> OnPostDescriptionAsync()
      {
         try
         {
            if (ModelState.IsValid)
            {
               objectiveService.Patch(Objective, nameof(Objective.Description));
               await objectiveService.SaveChanges();
               ActiveProjectId = Objective.PtprojectId;
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
            objectiveService.Patch(Objective, nameof(Objective.IsCompleted));
            await objectiveService.SaveChanges();
         }
         catch (Exception ex)
         {
            Message = ex.Message;
         }
      }

      public async Task OnPostDeleteAsync()
      {
         objectiveService.Remove(Objective.Id);
         await objectiveService.SaveChanges();
      }

      public async Task<IActionResult> OnPostAddAsync()
      {
         try
         {
            objectiveService.Create(Objective);
            await objectiveService.SaveChanges();
            ActiveProjectId = Objective.PtprojectId;
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