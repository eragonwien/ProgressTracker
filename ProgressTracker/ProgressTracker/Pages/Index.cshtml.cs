using System;
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
        public int UserId { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public IndexModel(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public void OnGet()
        {
            UserId = UserId > 0 ? UserId : Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Projects = projectService.GetAll(UserId).Select(p => new ProjectViewModel(p));
        }
    }
}
