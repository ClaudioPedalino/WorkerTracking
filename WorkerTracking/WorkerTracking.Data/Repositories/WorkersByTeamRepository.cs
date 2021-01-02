using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class WorkersByTeamRepository : IWorkersByTeamRepository
    {
        private readonly DataContext _context;

        public WorkersByTeamRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkersByTeam>> GetAllWorkersWithTeamInfo()
        {
            var result = await _context.WorkersByTeams
                   .Include(x => x.Team)
                   .Include(x => x.Worker)
                   .Include(x => x.Worker.Status)
                   .Include(x => x.Worker.Role)
                   .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WorkersByTeam>> GetWorkerTeamInfo(Guid workerId)
        {
            var result = await _context.WorkersByTeams
                   .Where(x => x.WorkerId == workerId)
                   .Include(x => x.Team)
                   .Include(x => x.Worker)
                   .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<WorkersByTeam>> GetTotalWorkersByTeam()
            => await _context.WorkersByTeams
                             .Include(x => x.Team)
                             .OrderBy(x => x.Team.Name)
                             .ToListAsync();

        public async Task CreateWorkerByTeam(WorkersByTeam entity)
        {
            await _context.WorkersByTeams.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

    }
}
