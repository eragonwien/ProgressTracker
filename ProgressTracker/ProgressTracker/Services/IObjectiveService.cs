using ProgressTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Services
{
    public interface IObjectiveService
    {
        IEnumerable<Ptobjective> GetAll();
        Task<Ptobjective> GetOne(int id);
        void Create(Ptobjective objective);
        void Update(Ptobjective objective);
        void Patch(Ptobjective objective, params string[] columns);
        void Remove(int id);
        bool Exists(int id);
        Task SaveChanges();
    }
}
