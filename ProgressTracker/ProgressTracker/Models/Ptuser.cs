using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
    public partial class Ptuser
    {
      private string v1;
      private bool v2;

      public Ptuser()
        {
            Ptproject = new HashSet<Ptproject>();
        }

      public Ptuser(string email, string v1, bool v2)
      {
         Email = email;
         this.v1 = v1;
         this.v2 = v2;
      }

      public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GoogleId { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Ptproject> Ptproject { get; set; }
    }
}
