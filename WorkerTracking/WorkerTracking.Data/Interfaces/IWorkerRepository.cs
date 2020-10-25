using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAllWorkersAsync();
        Task<string> UpdateWorkerStatusAsync(Guid workerId, int statusId);
    }
}