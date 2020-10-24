using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAllWorkersAsync();
    }
}