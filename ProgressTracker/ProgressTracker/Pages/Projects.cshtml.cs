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
         Projects = projectService.GetAll(UserId, true, false).Select(p => new ProjectViewModel(p));
      }

      public void OnGetClosed()
      {
         Projects = projectService.GetAll(UserId, false, true).Select(p => new ProjectViewModel(p));
      }
   }
}