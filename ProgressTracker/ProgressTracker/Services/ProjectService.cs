﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
   public class ProjectService : IProjectService
   {
      private readonly PROGRESSTRACKERContext context;

      public ProjectService(PROGRESSTRACKERContext context)
      {
         this.context = context;
      }

      public void Create(Ptproject project)
      {
         if (project.PtuserId <= 0)
         {
            throw new Exception("Projekt Benutzer Id ist leer");
         }
         project.IsActive = true;
         context.Ptproject.Add(project);
      }

      public void Patch(Ptproject project, params string[] columns)
      {
         if (columns == null || columns.Count() == 0)
         {
            return;
         }
         context.Ptproject.Attach(project);
         foreach (var column in columns)
         {
            if (project.GetType().GetProperty(column).GetValue(project) != null)
            {
               context.Entry(project).Property(column).IsModified = true;
            }
         }
      }

      public void Update(Ptproject project)
      {
         if (project.Id <= 0)
         {
            throw new Exception("Ungültige Projekt Id");
         }
         if (project.PtuserId <= 0)
         {
            throw new Exception("Ungültige Projekt Benutzer Id");
         }
         if (string.IsNullOrEmpty(project.Name))
         {
            throw new Exception("Projekt Name darf nicht leer sein");
         }
         context.Ptproject.Update(project);
      }

      public bool Exists(int id)
      {
         return context.Ptproject.Any(p => p.Id == id && p.IsActive);
      }

      public IEnumerable<Ptproject> GetAll(int userId)
      {
         return context.Ptproject
             .Include(p => p.Ptobjective)
             .Where(p => p.IsActive && (p.PtuserId == userId));
      }

      public Task<Ptproject> GetOne(int id)
      {
         return context.Ptproject
             .Include(p => p.Ptobjective)
             .SingleOrDefaultAsync(p => p.Id == id && p.IsActive);
      }

      public void Remove(int id)
      {
         var removeProject = GetOne(id).Result;
         if (removeProject != null)
         {
            removeProject.IsActive = false;
            Patch(removeProject, nameof(Ptproject.IsActive));
         }
      }

      public Task SaveChanges()
      {
         return context.SaveChangesAsync();
      }
   }
}
