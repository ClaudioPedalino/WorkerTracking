using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<Status> GetStatusByIdAsync(int StatusId);
        Task CreateStatusAsync(Status entity);
        Task DeleteStatusAsync(Status entity);
        Task<bool> IsBeingUsed(Status entity);
    }
}
