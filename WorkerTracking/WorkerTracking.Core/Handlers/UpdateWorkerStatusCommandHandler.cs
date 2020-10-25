using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class UpdateWorkerStatusCommandHandler : IRequestHandler<UpdateWorkerStatusCommand, string>
    {
        private readonly IWorkerRepository _workerRepository;

        public UpdateWorkerStatusCommandHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<string> Handle(UpdateWorkerStatusCommand command, CancellationToken cancellationToken)
        {
            var response = await _workerRepository.UpdateWorkerStatusAsync(command.WorkerId, command.StatusId);

            return response;
        }
    }
}
