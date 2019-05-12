using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProgressTracker.Models
{
    public partial class Ptobjective
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        public int PtprojectId { get; set; }
        [Required]
        [MinLength(3)]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }

        public virtual Ptproject Ptproject { get; set; }
    }
}
