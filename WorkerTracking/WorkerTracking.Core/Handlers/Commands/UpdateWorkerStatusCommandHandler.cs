using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class UpdateWorkerStatusCommandHandler : IRequestHandler<UpdateWorkerStatusCommand, BaseCommandResponse>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IStatusRepository _statusRepository;

        public UpdateWorkerStatusCommandHandler(IWorkerRepository workerRepository, IStatusRepository statusRepository)
        {
            _workerRepository = workerRepository;
            _statusRepository = statusRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateWorkerStatusCommand request, CancellationToken cancellationToken)
        {
            var workerToUpdate = await _workerRepository.GetWorkerByIdAsync(request.WorkerId);
            if (workerToUpdate == null)
                return new BaseCommandResponse("Worker does not exist");

            var newStatus = await _statusRepository.GetStatusByIdAsync(request.StatusId);
            if (newStatus == null)
                return new BaseCommandResponse("Status does not exist");

            await _workerRepository.UpdateWorkerStatusAsync(workerToUpdate.WorkerId, newStatus.StatusId);

            return new BaseCommandResponse($"Worker {workerToUpdate.FirstName} {workerToUpdate.LastName} updated to status {newStatus.Name}");
        }
    }
}
