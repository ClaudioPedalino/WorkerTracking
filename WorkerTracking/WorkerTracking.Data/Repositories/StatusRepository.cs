using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private DataContext _context;

        public StatusRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            var res = _context.Status;
            
            var result = await res.ToListAsync();
            return result;
        }

        public async Task<Status> GetStatusByIdAsync(int statusId)
            => await _context.Status
                .Where(x => x.StatusId == statusId)
                .FirstOrDefaultAsync();

        public async Task CreateStatusAsync(Status entity)
        {
            await _context.Status.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStatusAsync(Status entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBeingUsed(Status entity) 
            => await _context.Workers.AnyAsync(x => x.StatusId == entity.StatusId);
    }
}
