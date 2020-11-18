using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocationAsync()
            => await _context.Location.ToListAsync();

        public async Task<Location> GetLocationByIdAsync(Guid locationId)
        => await _context.Location
                .Where(x => x.LocationId == locationId)
                .FirstOrDefaultAsync();

        public async Task CreateLocationAsync(Location entity)
        {
            await _context.Location.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLocationAsync(Location entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //public async Task<bool> IsBeingUsed(Team entity) :TODO
        //{
        //    return await _context.WorkersByTeams.AnyAsync(x => x.TeamId == entity.TeamId);
        //}
    }
}
