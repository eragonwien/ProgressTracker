using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Project
    {
        public Project()
        {
            Objective = new HashSet<Objective>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Objective> Objective { get; set; }
    }
}
