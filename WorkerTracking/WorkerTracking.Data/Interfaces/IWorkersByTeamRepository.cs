using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface IWorkersByTeamRepository
    {
        Task CreateWorkerByTeam(WorkersByTeam entity);
    }
}
