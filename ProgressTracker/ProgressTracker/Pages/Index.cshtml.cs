using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgressTracker.Models;

namespace ProgressTracker.Pages
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        public void OnGet()
        {

        }
    }
}
