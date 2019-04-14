using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly PROGRESSTRACKERContext context;

        public ObjectiveService(PROGRESSTRACKERContext context)
        {
            this.context = context;
        }

        public void Create(Objective objective)
        {
            context.Objective.Add(objective);
        }

        public bool Exists(int id)
        {
            return context.Objective.Any(o => o.Id == id && o.IsActive);
        }

        public IEnumerable<Objective> GetAll()
        {
            return context.Objective.Where(o => o.IsActive);
        }

        public Task<Objective> GetOne(int id)
        {
            return context.Objective.SingleOrDefaultAsync(o => o.Id == id && o.IsActive);
        }

        public void Remove(int id)
        {
            Objective removeObjective = GetOne(id).Result;
            if (removeObjective != null)
            {
                removeObjective.IsActive = false;
                Update(removeObjective);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }

        public void Update(Objective objective)
        {
            context.Objective.Update(objective);
        }
    }
}
