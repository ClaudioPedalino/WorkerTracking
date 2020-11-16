using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, string>
    {
        private readonly IStatusRepository _statusRepository;

        public CreateStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<string> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var newStatus = new Status(name: request.StatusName);

            await _statusRepository.CreateStatusAsync(newStatus);

            return "Status created succesfully";
        }
    }
}
