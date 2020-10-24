using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private DataContext _context;

        public WorkerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Worker>> GetAllWorkersAsync()
        {
            List<Worker> response = await _context.Workers.ToListAsync();

            return response;
        }
    }
}
