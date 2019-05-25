using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProgressTracker.Models
{
   public partial class Ptproject
   {
      public Ptproject()
      {
         Ptobjective = new HashSet<Ptobjective>();
      }

      public int Id { get; set; }
      [Required]
      [Range(1, int.MaxValue)]
      public int PtuserId { get; set; }
      [Required]
      [MinLength(1)]
      public string Name { get; set; }
      public string Description { get; set; }
      public string Status { get; set; }
      public bool IsActive { get; set; }

      public virtual Ptuser Ptuser { get; set; }
      public virtual ICollection<Ptobjective> Ptobjective { get; set; }
   }
}
