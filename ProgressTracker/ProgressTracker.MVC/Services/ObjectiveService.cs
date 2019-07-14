using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.MVC.Models;

namespace ProgressTracker.MVC.Services
{
   public interface ITaskService
   {
      IEnumerable<Pttask> GetAll();
      Task<Pttask> GetOne(int id);
      void Create(Pttask task);
      void Update(Pttask task);
      void Patch(Pttask task, params string[] columns);
      void Remove(int id);
      bool Exists(int id);
      Task SaveChanges();
   }

   public class TaskService : ITaskService
   {
      private readonly PROGRESSTRACKERContext context;

      public TaskService(PROGRESSTRACKERContext context)
      {
         this.context = context;
      }

      public void Create(Pttask task)
      {
         if (task.PtprojectId <= 0)
         {
            throw new Exception("Projekt Id fehlt");
         }
         if (string.IsNullOrEmpty(task.Description))
         {
            throw new Exception("Project Beschreibung darf nicht leer sein");
         }
         task.Active = true;
         task.Completed = false;
         context.Pttask.Add(task);
      }

      public bool Exists(int id)
      {
         return context.Pttask.Any(o => o.Id == id && o.Active);
      }

      public IEnumerable<Pttask> GetAll()
      {
         return context.Pttask.Where(o => o.Active);
      }

      public Task<Pttask> GetOne(int id)
      {
         return context.Pttask.SingleOrDefaultAsync(o => o.Id == id && o.Active);
      }

      public void Patch(Pttask task, params string[] columns)
      {
         if (columns == null || columns.Count() == 0)
         {
            return;
         }
         context.Attach(task);
         foreach (var column in columns)
         {
            context.Entry(task).Property(column).IsModified = true;
         }
      }

      public void Remove(int id)
      {
         Pttask removeTask = GetOne(id).Result;
         if (removeTask != null)
         {
            removeTask.Active = false;
            Update(removeTask);
         }
      }

      public Task SaveChanges()
      {
         return context.SaveChangesAsync();
      }

      public void Update(Pttask task)
      {
         context.Pttask.Update(task);
      }
   }
}
