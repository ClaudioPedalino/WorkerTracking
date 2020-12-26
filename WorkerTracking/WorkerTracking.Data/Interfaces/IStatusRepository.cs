using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<Status> GetStatusByIdAsync(int statusId);
        Task CreateStatusAsync(Status entity);
        Task DeleteStatusAsync(Status entity);
        Task<bool> IsBeingUsed(Status entity);
    }
}
