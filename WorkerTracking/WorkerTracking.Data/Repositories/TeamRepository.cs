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

    }
}
