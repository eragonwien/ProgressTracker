using ProgressTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Services
{
    public interface IObjectiveService
    {
        IEnumerable<Objective> GetAll();
        Task<Objective> GetOne(int id);
        void Create(Objective objective);
        void Update(Objective objective);
        void Remove(int id);
        bool Exists(int id);
        Task SaveChanges();
    }
}
