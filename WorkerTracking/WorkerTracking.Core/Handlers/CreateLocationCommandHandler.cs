using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, string>
    {
        private readonly ILocationRepository _teamRepository;

        public CreateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _teamRepository = locationRepository;
        }

        public async Task<string> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var newLocation = new Location(name : request.LocationName);

            await _teamRepository.CreateLocationAsync(newLocation);

            return "Location created succesfully";
        }
    }
}
