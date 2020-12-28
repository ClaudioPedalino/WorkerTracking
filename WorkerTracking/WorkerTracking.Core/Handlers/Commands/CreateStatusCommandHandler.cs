using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, BaseCommandResponse>
    {
        private readonly IStatusRepository _statusRepository;

        public CreateStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var newStatus = new Status(name: request.Name);

            await _statusRepository.CreateStatusAsync(newStatus);

            return new BaseCommandResponse($"Status {newStatus.Name} created succesfully");
        }
    }
}
