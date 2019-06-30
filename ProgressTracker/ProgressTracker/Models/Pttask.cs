using System.ComponentModel.DataAnnotations;

namespace ProgressTracker.Models
{
   public partial class Pttask
   {
      public int Id { get; set; }
      [Required]
      public int PtprojectId { get; set; }
      [Required]
      public string Description { get; set; }
      public bool Completed { get; set; }
      public bool Active { get; set; }

      public virtual Ptproject Ptproject { get; set; }

      public Pttask()
      {
      }

      public Pttask(Ptproject ptproject)
      {
         PtprojectId = ptproject.Id;
      }
   }
}
