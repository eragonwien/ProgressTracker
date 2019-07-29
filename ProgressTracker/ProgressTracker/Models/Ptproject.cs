using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Ptproject
    {
        public Ptproject()
        {
            Pttask = new HashSet<Pttask>();
        }

        public int Id { get; set; }
        public int PtuserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }

        public virtual Ptuser Ptuser { get; set; }
        public virtual ICollection<Pttask> Pttask { get; set; }
    }
}
