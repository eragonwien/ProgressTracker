using System.ComponentModel.DataAnnotations;

namespace ProgressTracker.MVC.Models
{
   public partial class Pttask
   {
      public int Id { get; set; }
      public int PtprojectId { get; set; }
      [Required]
      [MaxLength(50)]
      public string Description { get; set; }
      public bool Completed { get; set; }
      public bool Active { get; set; }

      public virtual Ptproject Ptproject { get; set; }
   }
}
