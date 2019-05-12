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

        public void Create(Ptproject project)
        {
            context.Ptproject.Add(project);
        }

        public void Update(Ptproject project)
        {
            context.Ptproject.Update(project);
        }

        public bool Exists(int id)
        {
            return context.Ptproject.Any(p => p.Id == id && p.IsActive);
        }

        public IEnumerable<Ptproject> GetAll(int userId)
        {
            return context.Ptproject
                .Where(p => p.IsActive && (p.PtuserId == userId ));
        }

        public Task<Ptproject> GetOne(int id)
        {
            return context.Ptproject
                .Include(p => p.Ptobjective)
                .SingleOrDefaultAsync(p => p.Id == id && p.IsActive);
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
