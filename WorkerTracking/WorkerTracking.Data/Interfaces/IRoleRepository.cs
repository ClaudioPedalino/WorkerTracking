using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(int RoleId);
        Task CreateRoleAsync(Role entity);
        Task DeleteRoleAsync(Role entity);
        Task<bool> IsBeingUsed(Role entity);
    }
}