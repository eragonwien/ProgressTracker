using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProgressTracker.MVC.Models
{
   public partial class Ptuser
   {
      public Ptuser()
      {
         Ptproject = new HashSet<Ptproject>();
      }

      public int Id { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      [MaxLength(50)]
      public string Name { get; set; }
      [MaxLength(100)]
      public string Description { get; set; }
      public string GoogleId { get; set; }
      public bool Active { get; set; }

      public virtual ICollection<Ptproject> Ptproject { get; set; }
   }
}
