using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Pttask
    {
        public int Id { get; set; }
        public int PtobjectiveId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }

        public virtual Ptobjective Ptobjective { get; set; }
    }
}
