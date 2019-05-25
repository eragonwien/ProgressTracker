using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
   public class ObjectiveService : IObjectiveService
   {
      private readonly PROGRESSTRACKERContext context;

      public ObjectiveService(PROGRESSTRACKERContext context)
      {
         this.context = context;
      }

      public void Create(Ptobjective objective)
      {
         if (objective.PtprojectId <= 0)
         {
            throw new Exception("Projekt Id fehlt");
         }
         if (string.IsNullOrEmpty(objective.Description))
         {
            throw new Exception("Project Beschreibung darf nicht leer sein");
         }
         objective.IsActive = true;
         objective.IsCompleted = false;
         context.Ptobjective.Add(objective);
      }

      public bool Exists(int id)
      {
         return context.Ptobjective.Any(o => o.Id == id && o.IsActive);
      }

      public IEnumerable<Ptobjective> GetAll()
      {
         return context.Ptobjective.Where(o => o.IsActive);
      }

      public Task<Ptobjective> GetOne(int id)
      {
         return context.Ptobjective.SingleOrDefaultAsync(o => o.Id == id && o.IsActive);
      }

      public void Patch(Ptobjective objective, params string[] columns)
      {
         if (columns == null || columns.Count() == 0)
         {
            return;
         }
         context.Ptobjective.Attach(objective);
         foreach (var column in columns)
         {
            context.Entry(objective).Property(column).IsModified = true;
         }
      }

      public void Remove(int id)
      {
         Ptobjective removeObjective = GetOne(id).Result;
         if (removeObjective != null)
         {
            removeObjective.IsActive = false;
            Update(removeObjective);
         }
      }

      public Task SaveChanges()
      {
         return context.SaveChangesAsync();
      }

      public void Update(Ptobjective objective)
      {
         context.Ptobjective.Update(objective);
      }
   }
}
