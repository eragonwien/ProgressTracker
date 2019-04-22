using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public class TaskService : ITaskService
    {
        private readonly PROGRESSTRACKERContext context;

        public TaskService(PROGRESSTRACKERContext context)
        {
            this.context = context;
        }

        public void Create(Pttask task)
        {
            context.Pttask.Add(task);
        }

        public void Update(Pttask task)
        {
            context.Pttask.Update(task);
        }

        public bool Exists(int id)
        {
            return context.Pttask.Any(j => j.Id == id);
        }

        public Task<Pttask> GetOne(int id)
        {
            return context.Pttask.Where(j => j.Id == id && j.IsActive).SingleOrDefaultAsync();
        }

        public IEnumerable<Pttask> GetAll()
        {
            return context.Pttask.Where(j => j.IsActive);
        }

        public void Remove(int id)
        {
            Pttask removePttask = GetOne(id).Result;
            if (removePttask != null)
            {
                removePttask.IsActive = false;
                Update(removePttask);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }
    }
}
