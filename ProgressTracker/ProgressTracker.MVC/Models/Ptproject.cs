using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProgressTracker.MVC.Models
{
   public partial class Ptproject
   {
      public Ptproject()
      {
         Pttask = new HashSet<Pttask>();
      }

      public int Id { get; set; }
      public int PtuserId { get; set; }
      [Required]
      [MaxLength(50)]
      public string Name { get; set; }
      [MaxLength(100)]
      public string Description { get; set; }
      [MaxLength(20)]
      public string Status { get; set; }
      public bool Active { get; set; }

      public virtual Ptuser Ptuser { get; set; }
      public virtual ICollection<Pttask> Pttask { get; set; }
   }
}
