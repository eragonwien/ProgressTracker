using ProgressTracker.Models;
using ProgressTracker.Services;
using System.Collections.Generic;
using System.Linq;

namespace ProgressTracker.Pages
{
   public class ProjectsModel : BasePageModel
   {
      private readonly IProjectService projectService;
      public IEnumerable<ProjectViewModel> Projects { get; set; }

      public ProjectsModel(IProjectService projectService)
      {
         this.projectService = projectService;
      }

      public void OnGet()
      {
         Projects = projectService
            .GetAll(UserId)
            .Where(p => p.Active)
            .Select(p => new ProjectViewModel(p))
            .Where(p => p.Status.Equals(ProjectStatus.InProgress) || p.Status.Equals(ProjectStatus.Saved));
      }

      public void OnGetClosed()
      {
         Projects = projectService
            .GetAll(UserId)
            .Where(p => p.Active)
            .Select(p => new ProjectViewModel(p))
            .Where(p => p.Status.Equals(ProjectStatus.Completed));
      }

      public void OnGetDeleted()
      {
         Projects = projectService
            .GetAll(UserId)
            .Where(p => !p.Active)
            .Select(p => new ProjectViewModel(p));
      }
   }
}