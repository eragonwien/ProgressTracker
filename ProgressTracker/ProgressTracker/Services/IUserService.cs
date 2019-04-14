using ProgressTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressTracker.Services
{
    public interface IUserService
    {
        Task<User> GetOne(int id);
        Task<User> GetOne(string email);
        Task<User> Login(string email, string password);
        void Register(User user);
        void Edit(User user);
        void Remove(int id);
        bool Exists(int id);
        bool Exists(string email);
        Task SaveChanges();
    }
}
