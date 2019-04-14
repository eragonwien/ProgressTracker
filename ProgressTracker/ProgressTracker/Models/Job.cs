using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Job
    {
        public int Id { get; set; }
        public int ObjectiveId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }

        public virtual Objective Objective { get; set; }
    }
}
