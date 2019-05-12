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
    public class ObjectiveModel : BasePageModel
    {
        private readonly IObjectiveService oService;

        public ObjectiveModel(IObjectiveService objectiveService)
        {
            this.oService = objectiveService;
        }

        [BindProperty]
        public Ptobjective Objective { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task OnGetAsync()
        {
            IsCreate = false;
            Objective = await oService.GetOne(Id);
            ReturnUrl = Request.Headers["Referer"].ToString();
        }

        public void OnGetCreate()
        {
            IsCreate = true;
            ReturnUrl = "/";
        }

        public async Task OnPostDescriptionAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return;
                }
                oService.Patch(Objective, nameof(Objective.Description));
                await oService.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task OnPostStatusAsync()
        {
            try
            {
                oService.Patch(Objective, nameof(Objective.IsCompleted));
                await oService.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task OnPostDeleteAsync()
        {
            oService.Remove(Objective.Id);
            await oService.SaveChanges();
        }
    }
}