using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Ptproject
    {
        public Ptproject()
        {
            Ptobjective = new HashSet<Ptobjective>();
        }

        public int Id { get; set; }
        public int PtuserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

        public virtual Ptuser Ptuser { get; set; }
        public virtual ICollection<Ptobjective> Ptobjective { get; set; }
    }
}
