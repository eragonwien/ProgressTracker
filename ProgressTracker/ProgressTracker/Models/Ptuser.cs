﻿using System;
using System.Collections.Generic;

namespace ProgressTracker.Models
{
   public partial class Ptuser
   {
      public Ptuser()
      {
         Ptproject = new HashSet<Ptproject>();
      }

      public Ptuser(string email, string name, bool active = false, string password = null, string description = null, string googleId = null)
      {
         Email = email;
         Name = name;
         Active = active;
         Password = password;
         Description = description;
         GoogleId = googleId;
         Ptproject = new HashSet<Ptproject>();
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
