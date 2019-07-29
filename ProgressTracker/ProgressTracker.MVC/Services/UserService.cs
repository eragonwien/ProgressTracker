using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.MVC.Models;
using SNGCommon.Authentication;
using SNGCommon;
using SNGCommon.Models;
using SNGCommon.Resources;

namespace ProgressTracker.MVC.Services
{
   public interface IUserService
   {
      Task<Ptuser> GetOne(int id);
      Task<Ptuser> GetOne(string email);
      Task<AuthenticationResult> AuthenticateAsync(string email, string password);
      void Register(Ptuser user, bool isExternal = false);
      void Update(Ptuser user);
      void Remove(int id);
      bool Exists(int id);
      bool Exists(string email);
      Task SaveChanges();
   }

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

      public async Task<AuthenticationResult> AuthenticateAsync(string email, string password)
      {
         var user = await GetOne(email);
         if (user == null)
         {
            return new AuthenticationResult(false, Translation.UserNotFound);
         }

         if (!Authentication.IsPasswordValid(password, user.Password))
         {
            return new AuthenticationResult(false, Translation.WrongPassword);
         }
         return new AuthenticationResult(true, Translation.UserNotFound, user.Id, user.Name, user.Email);
      }

      public void Register(Ptuser user, bool isExternal = false)
      {
         user.Active = true;
         user.Password = !isExternal ? Authentication.GetEncodedPassword(user.Password) : string.Empty;

         if (Exists(user.Email))
         {
            throw new Exception("User exists. Choose another email");
         }
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
