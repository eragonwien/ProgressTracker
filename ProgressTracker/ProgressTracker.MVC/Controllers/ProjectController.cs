using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProgressTracker.MVC.Models;
using ProgressTracker.MVC.Services;
using SNGCommon.Resources;
using System.Linq;

namespace ProgressTracker.MVC.Controllers
{
   [Authorize]
   public class ProjectController : PTBaseController
   {
      private readonly IProjectService projectService;
      private readonly IStringLocalizer<Translation> localizer;

      public ProjectController(IProjectService projectService, IStringLocalizer<Translation> localizer) : base()
      {
         this.projectService = projectService;
         this.localizer = localizer;
      }

      // GET: Project
      public IActionResult Index()
      {
         var models = projectService.
            GetAll(UserId)
            .Where(p => p.Active)
            .Select(p => new ProjectViewModel(p))
            .Where(p => p.Status == ProjectStatus.InProgress || p.Status == ProjectStatus.Saved);
         return View(models);
      }

      // GET: Project/Details/5
      public ActionResult Details(int id)
      {
         return View();
      }

      // GET: Project/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: Project/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(IFormCollection collection)
      {
         try
         {
            // TODO: Add insert logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }

      // GET: Project/Edit/5
      public ActionResult Edit(int id)
      {
         return View();
      }

      // POST: Project/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(int id, IFormCollection collection)
      {
         try
         {
            // TODO: Add update logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }

      // GET: Project/Delete/5
      public ActionResult Delete(int id)
      {
         return View();
      }

      // POST: Project/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Delete(int id, IFormCollection collection)
      {
         try
         {
            // TODO: Add delete logic here

            return RedirectToAction(nameof(Index));
         }
         catch
         {
            return View();
         }
      }
   }
}