using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Models
{
    public class DetailNavbarViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public bool IsEditing { get; set; }
    }

    public class ProjectViewModel
    {
        public ProjectViewModel(Ptproject project)
        {
            Project = project;
            SetProgress(project);
        }

        public Ptproject Project { get; set; }
        public decimal Progress { get; set; }
        public int UnresolvedCount { get; set; }

        private void SetProgress(Ptproject project)
        {
            decimal progress = -1;
            try
            {
                int total = project.Ptobjective.Count;
                int completed = project.Ptobjective.Count(o => o.IsCompleted);
                progress = completed / total;
                Progress = progress >= 0 && progress <= 1 ? progress : Progress;
                UnresolvedCount = total - completed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
