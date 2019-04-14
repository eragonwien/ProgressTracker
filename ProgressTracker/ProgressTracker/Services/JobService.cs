using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public class JobService : IJobService
    {
        private readonly PROGRESSTRACKERContext context;

        public JobService(PROGRESSTRACKERContext context)
        {
            this.context = context;
        }

        public void Create(Job job)
        {
            context.Job.Add(job);
        }

        public void Update(Job job)
        {
            context.Job.Update(job);
        }

        public bool Exists(int id)
        {
            return context.Job.Any(j => j.Id == id);
        }

        public Task<Job> GetOne(int id)
        {
            return context.Job.Where(j => j.Id == id && j.IsActive).SingleOrDefaultAsync();
        }

        public IEnumerable<Job> GetAll()
        {
            return context.Job.Where(j => j.IsActive);
        }

        public void Remove(int id)
        {
            Job removeJob = GetOne(id).Result;
            if (removeJob != null)
            {
                removeJob.IsActive = false;
                Update(removeJob);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }
    }
}
