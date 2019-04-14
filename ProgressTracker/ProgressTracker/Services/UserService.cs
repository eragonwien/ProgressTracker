using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;
using SNGCommon.Authentication;

namespace ProgressTracker.Services
{
    public class UserService : IUserService
    {
        private readonly PROGRESSTRACKERContext context;

        public UserService(PROGRESSTRACKERContext context)
        {
            this.context = context;
        }

        public void Edit(User user)
        {
            context.User.Update(user);
        }

        public bool Exists(int id)
        {
            return context.User.Any(u => u.Id == id && u.IsActive);
        }

        public bool Exists(string email)
        {
            return context.User.Any(u => u.Email == email && u.IsActive);
        }

        public Task<User> GetOne(int id)
        {
            return context.User.SingleOrDefaultAsync(u => u.Id == id && u.IsActive);
        }

        public Task<User> GetOne(string email)
        {
            return context.User.SingleOrDefaultAsync(u => u.Email == email && u.IsActive);
        }

        public Task<User> Login(string email, string password)
        {
            var user = GetOne(email);
            if (user.Result == null)
            {
                throw new Exception("Benuzter wird nicht gefunden");
            }
            
            if (!Authentication.IsPasswordValid(password, user.Result.Password))
            {
                throw new Exception("Authentifizerung fehlgeschlagen");
            }

            return user;
        }

        public void Register(User user)
        {
            user.IsActive = false;
            context.User.Add(user);
        }

        public void Remove(int id)
        {
            User removeUser = context.User.SingleOrDefault(u => u.Id == id && u.IsActive);
            if (removeUser != null)
            {
                removeUser.IsActive = false;
                Edit(removeUser);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }
    }
}
