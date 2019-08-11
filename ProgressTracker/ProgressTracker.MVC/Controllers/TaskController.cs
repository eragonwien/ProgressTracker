using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ProgressTracker.MVC.Models;
using ProgressTracker.MVC.Services;
using SNGCommon.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.MVC.Controllers
{
   public class TaskController : PTBaseController
   {
      private readonly ITaskService taskService;
      private readonly IProjectService projectService;
      private readonly IStringLocalizer<Translation> localizer;

      public TaskController(ITaskService taskService, IProjectService projectService, IStringLocalizer<Translation> localizer, ILogger<PTBaseController> log) : base(log)
      {
         this.projectService = projectService;
         this.taskService = taskService;
         this.localizer = localizer;
      }

      // GET: Task/Add
      public IActionResult Add(int projectId = 0, string returnUrl = null)
      {
         var model = new AddTaskViewModel
         {
            ProjectIdList = projectService.GetAll(UserId).Where(p => p.Active).Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList()
         };
         if (projectId > 0)
         {
            model.ProjectIdList.RemoveAll(i => i.Value != projectId.ToString());
            model.ProjectId = projectId;
         }
         SetReturnUrl(returnUrl);
         return View(model);
      }

      // POST: Task/Add
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> Add([FromForm] AddTaskViewModel model, [FromForm] string returnUrl)
      {
         try
         {
            if (ModelState.IsValid && projectService.Exists(model.ProjectId))
            {
               taskService.Create(new Pttask { Description = model.Description, PtprojectId = model.ProjectId });
               await taskService.SaveChanges();
               return RedirectToAction("Details", "Project", new { id = model.ProjectId });
            }
         }
         catch (Exception ex)
         {
            ModelState.AddModelError("", ex.Message);
         }
         SetReturnUrl(returnUrl);
         return View(model);
      }

      // GET: Task/Details/5
      public async Task<IActionResult> Details(int id, string returnUrl)
      {
         var ptask = await taskService.GetOne(id);
         if (ptask == null)
         {
            return RedirectLocal(returnUrl);
         }
         SetReturnUrl(returnUrl);
         return View(ptask);
      }

      // POST: Task/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit([FromForm][Bind("Id", "Description", "PtprojectId")] Pttask pttask, [FromForm] string returnUrl)
      {
         if (ModelState.IsValid)
         {
            try
            {
               taskService.Patch(pttask, nameof(Pttask.Description));
               await taskService.SaveChanges();
               return RedirectToAction("Details", "Project", new { id = pttask.PtprojectId, returnUrl });
            }
            catch (Exception ex)
            {
               ModelState.AddModelError("", ex.Message);
            }
         }
         return await Details(pttask.Id, returnUrl);
      }

      // POST: Task/Check/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task Check(int id, bool completed)
      {
         var task = await taskService.GetOne(id);
         if (task != null && task.Id == id)
         {
            task.Completed = completed;
            taskService.Patch(task, nameof(task.Completed));
            await taskService.SaveChanges();
         }
      }

      // POST: Task/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Delete(int id, string returnUrl)
      {
         try
         {
            taskService.Remove(id);
            await taskService.SaveChanges();
         }
         catch (Exception ex)
         {
            log.LogError("Fehler beim Löschen von Task {0}: {1}", id, ex.Message);
            return RedirectToAction("Details", "Task", new { id, returnUrl });
         }
         return RedirectLocal(returnUrl);
      }
   }
}