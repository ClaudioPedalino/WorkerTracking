using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllWorkersAsync()
            => await _context.Teams.ToListAsync();

        public async Task<Team> GetTeamByIdAsync(Guid teamId)
        => await _context.Teams
                .Where(x => x.TeamId == teamId)
                .FirstOrDefaultAsync();

        public async Task CreateTeamAsync(Team entity)
        {
            _context.Teams.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(Team entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //public async Task<bool> IsBeingUsed(Team entity) :TODO
        //{
        //    return await _context.WorkersByTeams.AnyAsync(x => x.TeamId == entity.TeamId);
        //}
    }
}
