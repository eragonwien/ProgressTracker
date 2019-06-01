using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;
using SNGCommon.Authentication;
using SNGCommon.Resources;

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
            return context.Ptuser.Any(u => u.Id == id && u.Active);
        }

        public bool Exists(string email)
        {
            return context.Ptuser.Any(u => u.Email == email && u.Active);
        }

        public Task<Ptuser> GetOne(int id)
        {
            return context.Ptuser.SingleOrDefaultAsync(u => u.Id == id && u.Active);
        }

        public Task<Ptuser> GetOne(string email)
        {
            return context.Ptuser.SingleOrDefaultAsync(u => u.Email == email && u.Active);
        }

        public Task<Ptuser> Authenticate(string email, string password)
        {
            var user = GetOne(email);
            if (user.Result == null)
            {
                throw new Exception(Translation.ValidationUserNotFound);
            }
            
            if (!Authentication.IsPasswordValid(password, user.Result.Password))
            {
                throw new Exception(Translation.ValidationPasswordMismatch);
            }

            return user;
        }

        public void Register(Ptuser user, bool isExternal = false)
        {
            user.Active = true;
            user.Password = !isExternal ? Authentication.GetEncodedPassword(user.Password) : string.Empty;
            context.Ptuser.Add(user);
        }

        public void Remove(int id)
        {
            Ptuser removeUser = context.Ptuser.SingleOrDefault(u => u.Id == id && u.Active);
            if (removeUser != null)
            {
                removeUser.Active = false;
                Update(removeUser);
            }
        }

        public Task SaveChanges()
        {
            return context.SaveChangesAsync();
        }
    }
}
