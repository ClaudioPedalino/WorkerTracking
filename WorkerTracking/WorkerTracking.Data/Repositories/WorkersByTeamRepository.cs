using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task CreateWorkerByTeam(WorkersByTeam entity)
        {
            await _context.WorkersByTeams.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

    }
}
