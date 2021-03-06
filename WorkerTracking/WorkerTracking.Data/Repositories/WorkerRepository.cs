﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly DataContext _context;

        public WorkerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Worker>> GetAllWorkersAsync()
            => await _context.Workers
                .Where(x => x.IsActive)
                .Include(x => x.Status)
                .Include(x => x.Role)
                .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                .ToListAsync();

        public async Task<Worker> GetWorkerByIdAsync(Guid WorkerId)
            => await _context.Workers
                .Where(x => x.WorkerId == WorkerId)
                .Include(x => x.Status)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        public async Task<Worker> GetWorkerByUserNameAsync(string userName)
            => await _context.Workers
                .Where(x => x.Email.ToUpper() == userName.ToUpper())
                .Include(x => x.Status)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();

        public async Task CreateWorkerAsync(Worker entity)
        {
            await _context.Workers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWorkerStatusAsync(Guid workerId, int statusId)
        {
            var workerDb = _context.Workers
                                   .FirstOrDefault(x => x.WorkerId == workerId);

            workerDb.StatusId = statusId;
            workerDb.LastModificationTime = DateTime.Now;
            _context.Update(workerDb);
            await _context.SaveChangesAsync();
        }

    }
}
