using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly PROGRESSTRACKERContext context;

        public ProjectService(PROGRESSTRACKERContext context)
        {
            this.context = context;
        }

        public void Create(Project project)
        {
            context.Project.Add(project);
        }

        public void Update(Project project)
        {
            context.Project.Update(project);
        }

        public bool Exists(int id)
        {
            return context.Project.Any(p => p.Id == id && p.IsActive);
        }

        public IEnumerable<Project> GetAll()
        {
            return context.Project.Where(p => p.IsActive);
        }

        public Task<Project> GetOne(int id)
        {
            return context.Project.SingleOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public void Remove(int id)
        {
            var removeProject = GetOne(id).Result;
            if (removeProject != null)
            {
                removeProject.IsActive = false;
                Update(removeProject);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }
    }
}
