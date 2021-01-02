using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Core.Enums;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
            => await _context.Roles
                             .OrderBy(x => x.Name)
                             .ToListAsync();

        public async Task<Role> GetRoleByIdAsync(int RoleId)
            => await _context.Roles
                .FirstOrDefaultAsync(x => x.RoleId == RoleId);

        public async Task<Role> GetDefaultRole()
        {
            var defaultValue = await _context.Roles.FirstOrDefaultAsync(x => x.Name.Replace(" ","") == RolesEnum.NotAssigned.ToString());
            _context.Entry(defaultValue).State = EntityState.Detached;
            return defaultValue;
        }

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
            => await _context.Workers
                             .AnyAsync(x => x.RoleId == entity.RoleId);
    }
}
