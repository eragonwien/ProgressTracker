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

        public void Update(Ptuser user)
        {
            context.Ptuser.Update(user);
        }

        public bool Exists(int id)
        {
            return context.Ptuser.Any(u => u.Id == id && u.IsActive);
        }

        public bool Exists(string email)
        {
            return context.Ptuser.Any(u => u.Email == email && u.IsActive);
        }

        public Task<Ptuser> GetOne(int id)
        {
            return context.Ptuser.SingleOrDefaultAsync(u => u.Id == id && u.IsActive);
        }

        public Task<Ptuser> GetOne(string email)
        {
            return context.Ptuser.SingleOrDefaultAsync(u => u.Email == email && u.IsActive);
        }

        public Task<Ptuser> Login(string email, string password)
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

        public void Register(Ptuser user)
        {
            user.IsActive = false;
            user.Password = Authentication.GetEncodedPassword(user.Password);
            context.Ptuser.Add(user);
        }

        public void Remove(int id)
        {
            Ptuser removeUser = context.Ptuser.SingleOrDefault(u => u.Id == id && u.IsActive);
            if (removeUser != null)
            {
                removeUser.IsActive = false;
                Update(removeUser);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }
    }
}
