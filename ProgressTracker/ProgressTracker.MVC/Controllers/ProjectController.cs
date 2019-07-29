using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
   [Authorize]
   public class ProjectController : PTBaseController
   {
      private readonly IProjectService projectService;
      private readonly IStringLocalizer<Translation> localizer;

      public ProjectController(IProjectService projectService, IStringLocalizer<Translation> localizer, ILogger<PTBaseController> log) : base(log)
      {
         this.projectService = projectService;
         this.localizer = localizer;
      }

      // GET: Project
      public IActionResult Index(string action, bool closed = false, bool deleted = false)
      {
         var models = projectService.
            GetAll(UserId)
            .Where(p => (!deleted && p.Active) || (deleted && !p.Active))
            .Select(p => new ProjectViewModel(p))
            .Where(p => deleted || (!deleted && closed && p.Status == ProjectStatus.Completed) || (!deleted && !closed && (p.Status == ProjectStatus.InProgress || p.Status == ProjectStatus.Saved)));
         ViewBag.ShowEmptyListPlaceHolder = !closed && !deleted;
         string title = closed ? localizer[Translation.ClosedProjects] : deleted ? localizer[Translation.DeletedProjects] : localizer[Translation.AllListing];
         ViewBag.Title = title;
         return View(models);
      }

      // GET: Project/Details/5
      public IActionResult Details(int id)
      {
         var project = projectService.GetOne(id);
         var model = new ProjectViewModel(project);
         ViewBag.AddLink = Url.Action("Add", "Task", new { projectId = project.Id });
         ViewBag.EditLink = Url.Action("Edit", "Project", new { id });
         if (project.Active)
         {
            ViewBag.DeleteLink = Url.Action("Delete", "Project", new { id });
         }
         else
         {
            ViewBag.RestoreLink = Url.Action("Restore", "Project", new { id });
         }
         return View(model);
      }

      // GET: Project/Add
      public ActionResult Add()
      {
         var model = new Ptproject { PtuserId = UserId };
         return View(model);
      }

      // POST: Project/Add
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Add([FromForm] Ptproject project)
      {
         try
         {
            projectService.Create(project);
            await projectService.SaveChanges();
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            log.LogError("Fehler bei Hinzufügen von Project: {0}", ex.Message);
            return View(project);
         }
      }

      // GET: Project/Edit/5
      public ActionResult Edit(int id)
      {
         var model = projectService.GetOne(id);
         return View(model);
      }

      // POST: Project/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit([FromForm] Ptproject project)
      {
         try
         {
            projectService.Update(project);
            await projectService.SaveChanges();
            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View(project);
         }
      }

      // POST: Project/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Delete(int id)
      {
         try
         {
            projectService.Remove(id);
            await projectService.SaveChanges();
         }
         catch (Exception ex)
         {
            log.LogError("Fehler beim Löschen von Projekt {0}: {1}", id, ex.Message);
         }
         return RedirectToAction(nameof(Index));
      }

      // POST: Project/Restore/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Restore(int id)
      {
         try
         {
            projectService.Restore(id);
            await projectService.SaveChanges();
         }
         catch (Exception ex)
         {
            log.LogError("Fehler beim Reaktivierung von Projekt {0}: {1}", id, ex.Message);
         }
         return RedirectToAction(nameof(Index));
      }
   }
}