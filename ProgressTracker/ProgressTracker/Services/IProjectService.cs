using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Task<Project> GetOne(int id);
        void Create(Project project);
        void Update(Project project);
        void Remove(int id);
        bool Exists(int id);
        Task SaveChanges();
    }
}
