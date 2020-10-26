using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAllWorkersAsync();
        Task<Worker> GetWorkerByIdAsync(Guid WorkerId);
        Task<string> UpdateWorkerStatusAsync(Guid workerId, int statusId);
    }
}