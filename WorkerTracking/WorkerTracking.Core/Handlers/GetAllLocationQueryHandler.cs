using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using WorkerTracking.Api.Controllers;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, List<LocationModel>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetAllLocationQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;

        }

        public async Task<List<LocationModel>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
                                            
        {
            var teamsDb = await _locationRepository.GetAllLocationAsync();
            var response = new List<LocationModel>();

            response.AddRange(
                teamsDb.Select(x =>
                    new LocationModel(x.LocationId, x.LocationName)));

            return response;
        }
    }
}
