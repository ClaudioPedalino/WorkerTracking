using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IWorkersByTeamRepository
    {
        Task<IEnumerable<WorkersByTeam>> GetAllWorkersWithTeamInfo();
        Task CreateWorkerByTeam(WorkersByTeam entity);
    }
}
