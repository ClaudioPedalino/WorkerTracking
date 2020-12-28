using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(Guid teamId);

        Task CreateTeamAsync(Team entity);
        Task DeleteTeamAsync(Team entity);
        Task<bool> IsBeingUsed(Guid teamId);

    }
}
