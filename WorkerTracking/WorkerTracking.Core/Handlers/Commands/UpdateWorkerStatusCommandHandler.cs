using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class UpdateWorkerStatusCommandHandler : IRequestHandler<UpdateWorkerStatusCommand, BaseCommandResponse>
    {
        private readonly IUserStore<User> userStore;
        private readonly IWorkerRepository _workerRepository;
        private readonly IStatusRepository _statusRepository;

        public UpdateWorkerStatusCommandHandler(IWorkerRepository workerRepository, IStatusRepository statusRepository, IUserStore<User> userStore)
        {
            _workerRepository = workerRepository;
            _statusRepository = statusRepository;
            this.userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(UpdateWorkerStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new ArgumentNullException("User does not exists");
            if (user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

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
