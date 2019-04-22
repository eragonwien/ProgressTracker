using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public interface ITaskService
    {
        IEnumerable<Pttask> GetAll();
        Task<Pttask> GetOne(int id);
        void Create(Pttask task);
        void Update(Pttask task);
        void Remove(int id);
        bool Exists(int id);
        Task SaveChanges();
    }
}
