using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProgressTracker.Models;
using ProgressTracker.Services;
using SNGCommon.Resources;
using System.Threading.Tasks;

namespace ProgressTracker.Areas.Tasks.Pages
{
   public class IndexModel : BasePageModel
   {
      private readonly ITaskService taskService;
      private readonly IProjectService projectService;
      private readonly IStringLocalizer<Translation> localizer;

      [BindProperty]
      public Pttask Task { get; set; }

      [BindProperty(SupportsGet = true)]
      public int Id { get; set; }

      public IndexModel(ITaskService taskService, IProjectService projectService, IStringLocalizer<Translation> localizer)
      {
         this.taskService = taskService;
         this.projectService = projectService;
         this.localizer = localizer;
      }

      public async Task OnGetAsync()
      {
         Task = await taskService.GetOne(Id);
      }
   }
}