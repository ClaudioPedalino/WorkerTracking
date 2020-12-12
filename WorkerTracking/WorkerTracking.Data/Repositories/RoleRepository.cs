using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
            => await _context.Roles.ToListAsync();

        public async Task<Role> GetRoleByIdAsync(int RoleId)
            => await _context.Roles
                .Where(x => x.RoleId == RoleId)
                .FirstOrDefaultAsync();

        public async Task CreateRoleAsync(Role entity)
        {
            await _context.Roles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Role entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBeingUsed(Role entity)
            => await _context.Workers.AnyAsync(x => x.RoleId == entity.RoleId);
    }
}
