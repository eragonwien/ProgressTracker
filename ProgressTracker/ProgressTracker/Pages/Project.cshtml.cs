using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.Models;
using ProgressTracker.Services;

namespace ProgressTracker.Pages
{
    public class ProjectModel : BasePageModel
    {
        private readonly IProjectService pService;

        [BindProperty]
        public Ptproject Project { get; set; }
        public ProjectModel(IProjectService projectService)
        {
            this.pService = projectService;
        }

        public void OnGetCreate()
        {
            IsCreate = true;
            ReturnUrl = "/";
        }
    }
}