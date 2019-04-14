using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class User
    {
        public User()
        {
            Project = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
