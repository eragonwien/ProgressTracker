using System.Linq;

namespace ProgressTracker.MVC.Models
{
   public class ProjectViewModel
   {
      public Ptproject Project { get; set; }
      public decimal Progress { get; set; }
      public int UnresolvedCount { get; set; }
      public ProjectStatus Status { get; set; }

      public ProjectViewModel(Ptproject project)
      {
         Project = project;
         SetProgress();
         SetStatus();
      }

      private void SetStatus()
      {
         switch (Progress)
         {
            case 0:
               Status = ProjectStatus.Saved;
               break;
            case 1:
               Status = ProjectStatus.Completed;
               break;
            default:
               Status = ProjectStatus.InProgress;
               break;
         }
      }

      private void SetProgress()
      {
         int total = Project.Pttask.Count;
         int completed = Project.Pttask.Count(o => o.Completed);
         Progress = total > 0 ? (decimal)completed / total : 0;
         UnresolvedCount = total - completed;
      }
   }
}
