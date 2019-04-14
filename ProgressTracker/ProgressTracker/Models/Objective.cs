using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Objective
    {
        public Objective()
        {
            Job = new HashSet<Job>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Job> Job { get; set; }
    }
}
