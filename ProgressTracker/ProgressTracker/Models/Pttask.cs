using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Pttask
    {
        public int Id { get; set; }
        public int PtprojectId { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public bool Active { get; set; }

        public virtual Ptproject Ptproject { get; set; }
    }
}
