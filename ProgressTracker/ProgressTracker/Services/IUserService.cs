using ProgressTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Services
{
    public interface IUserService
    {
        Task<Ptuser> GetOne(int id);
        Task<Ptuser> GetOne(string email);
        Task<Ptuser> Login(string email, string password);
        void Register(Ptuser user);
        void Update(Ptuser user);
        void Remove(int id);
        bool Exists(int id);
        bool Exists(string email);
        Task SaveChanges();
    }
}
