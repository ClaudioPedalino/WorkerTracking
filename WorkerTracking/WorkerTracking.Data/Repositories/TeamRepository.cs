using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
            => await _context.Teams
                             .OrderBy(x => x.Name)
                             .ToListAsync();

        public async Task<Team> GetTeamByIdAsync(Guid teamId)
        => await _context.Teams
                .Where(x => x.TeamId == teamId)
                .FirstOrDefaultAsync();

        public async Task CreateTeamAsync(Team entity)
        {
            await _context.Teams.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(Team entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBeingUsed(Guid teamId)
        {
            return await _context.WorkersByTeams
                                 .Where(x => x.TeamId == teamId)
                                 .AnyAsync(x => x.Worker.IsActive);
        }
    }
}
