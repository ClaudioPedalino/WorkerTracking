using Microsoft.EntityFrameworkCore;
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
        private DataContext _context;

        public WorkerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Worker>> GetAllWorkersAsync() 
            => await _context.Workers
                .Where(x => x.IsActive)
                .Include(x => x.Status)
                .Include(x => x.Role)
                .Include(x => x.WorkersByTeamId)
                    .ThenInclude(y => y.Team)
                .ToListAsync();

        public async Task<Worker> GetWorkerByIdAsync(Guid WorkerId) 
            => await _context.Workers
                .Where(x => x.WorkerId == WorkerId)
                .Include(x => x.Status)
                .Include(x => x.Role)
                .Include(x => x.WorkersByTeamId)
                    .ThenInclude(y => y.Team)
                .FirstOrDefaultAsync();

        public async Task<string> UpdateWorkerStatusAsync(Guid workerId, int statusId)
        {
            var workerDb = _context.Workers.Where(x => x.WorkerId == workerId).FirstOrDefault();

            if (workerDb == null) 
                return "Worker Not Found";

            workerDb.StatusId = statusId;
            workerDb.LastModificationTime = DateTime.Now;
            _context.Update(workerDb);
            await _context.SaveChangesAsync();
            return "Worker Updated Correctly";
        }

        public async Task CreateWorkerAsync(Worker entity)
        {
            await _context.Workers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
