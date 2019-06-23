﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.Models;
using ProgressTracker.Services;

namespace ProgressTracker.Pages
{
   [Authorize]
   public class IndexModel : BasePageModel
   {
      private readonly IProjectService projectService;
      public IEnumerable<ProjectViewModel> Projects { get; set; }

      public IndexModel(IProjectService projectService)
      {
         this.projectService = projectService;
      }

      public IActionResult OnGet()
      {
         var userProjects = projectService.GetAll(UserId);
         if (userProjects.Count() > 0)
         {
            return Redirect(Url.Page("project", new { id = userProjects.First().Id }));
         }
         return Page();
      }
   }
}
