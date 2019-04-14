﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgressTracker.Models;

namespace ProgressTracker.Services
{
    public interface IJobService
    {
        IEnumerable<Job> GetAll();
        Task<Job> GetOne(int id);
        void Create(Job job);
        void Update(Job job);
        void Remove(int id);
        bool Exists(int id);
        Task SaveChanges();
    }
}