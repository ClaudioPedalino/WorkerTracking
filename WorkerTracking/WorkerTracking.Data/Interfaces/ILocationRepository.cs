using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocationAsync();
        Task<Location> GetLocationByIdAsync(Guid LocationId);

        Task CreateLocationAsync(Location entity);
        Task DeleteLocationAsync(Location entity);
        //Task<bool> IsBeingUsed(Team entity);

    }
}
