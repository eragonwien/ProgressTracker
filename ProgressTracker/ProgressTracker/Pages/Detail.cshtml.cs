using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProgressTracker.Models;
using ProgressTracker.Services;

namespace ProgressTracker.Pages
{
   [Authorize]
   public class DetailModel : BasePageModel
   {
      private readonly IProjectService projectService;

      [BindProperty(SupportsGet = true)]
      public int Id { get; set; }
      public Ptproject Project { get; set; }

      public DetailModel(IProjectService projectService)
      {
         this.projectService = projectService;
      }

      public async Task OnGetAsync()
      {
         Project = await projectService.GetOne(Id);
         ReturnUrl = Url.IsLocalUrl(ReturnUrl) ? ReturnUrl : null;
      }
   }
}