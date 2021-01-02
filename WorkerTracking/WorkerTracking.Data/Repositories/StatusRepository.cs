using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Core.Enums;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DataContext _context;

        public StatusRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Status>> GetAllStatusAsync()
            => await _context.Status
                             .OrderBy(x => x.Name)
                             .ToListAsync();

        public async Task<Status> GetStatusByIdAsync(int statusId)
            => await _context.Status
                .Where(x => x.StatusId == statusId)
                .FirstOrDefaultAsync();

        public async Task<Status> GetDefaultStatus()
        {
            var defaultValue = await _context.Status.FirstOrDefaultAsync(x => x.Name == StatusEnum.Inactive.ToString());
            _context.Entry(defaultValue).State = EntityState.Detached;
            return defaultValue;
        }

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
            => await _context.Workers
                             .AnyAsync(x => x.StatusId == entity.StatusId);
    }
}
