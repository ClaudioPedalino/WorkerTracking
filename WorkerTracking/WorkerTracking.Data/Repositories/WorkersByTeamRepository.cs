using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class WorkersByTeamRepository : IWorkersByTeamRepository
    {
        private DataContext _context;

        public WorkersByTeamRepository(DataContext context)
        {
            _context = context;
        }


        public async Task CreateWorkerByTeam(WorkersByTeam entity)
        {
            await _context.WorkersByTeams.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
