using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public interface IProjectService
    {
        IEnumerable<Ptproject> GetAll();
        Task<Ptproject> GetOne(int id);
        void Create(Ptproject project);
        void Update(Ptproject project);
        void Remove(int id);
        bool Exists(int id);
        Task SaveChanges();
    }
}
