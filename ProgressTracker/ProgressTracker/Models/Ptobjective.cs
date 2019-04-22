using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Ptobjective
    {
        public Ptobjective()
        {
            Pttask = new HashSet<Pttask>();
        }

        public int Id { get; set; }
        public int PtprojectId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

        public virtual Ptproject Ptproject { get; set; }
        public virtual ICollection<Pttask> Pttask { get; set; }
    }
}
