using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IWorkersByTeamRepository
    {
        Task<IEnumerable<WorkersByTeam>> GetAllWorkersWithTeamInfo();
        Task<IEnumerable<WorkersByTeam>> GetTotalWorkersByTeam();
        Task<IEnumerable<WorkersByTeam>> GetWorkerTeamInfo(Guid workerId);
        Task CreateWorkerByTeam(WorkersByTeam entity);
    }
}
