using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Models
{
    public class BasePageModel : PageModel
    {
        public string Message { get; set; }
        [ViewData]
        [BindProperty]
        public string ReturnUrl { get; set; }
    }
}
