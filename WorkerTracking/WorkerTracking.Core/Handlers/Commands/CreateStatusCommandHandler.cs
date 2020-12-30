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
    public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, BaseCommandResponse>
    {
        private readonly IUserStore<User> userStore;
        private readonly IStatusRepository _statusRepository;

        public CreateStatusCommandHandler(IStatusRepository statusRepository, IUserStore<User> userStore)
        {
            _statusRepository = statusRepository;
            this.userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new ArgumentNullException("User does not exists");
            if (user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var newStatus = new Status(name: request.Name);

            await _statusRepository.CreateStatusAsync(newStatus);

            return new BaseCommandResponse($"Status {newStatus.Name} created succesfully");
        }
    }
}
