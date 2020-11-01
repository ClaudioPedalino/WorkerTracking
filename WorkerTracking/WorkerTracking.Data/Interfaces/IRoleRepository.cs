using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role> GetRoleByIdAsync(int RoleId);
        Task CreateRoleAsync(Role entity);
        Task DeleteRoleAsync(Role roleId);
        Task<bool> IsBeingUsed(Role entity);
    }
}