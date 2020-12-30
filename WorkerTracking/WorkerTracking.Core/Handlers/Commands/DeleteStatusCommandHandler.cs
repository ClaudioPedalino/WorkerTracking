using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Exceptions;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, BaseCommandResponse>
    {
        private readonly IUserStore<User> userStore;
        private readonly IStatusRepository _satusRepository;

        public DeleteStatusCommandHandler(IStatusRepository satusRepository, IUserStore<User> userStore)
        {
            _satusRepository = satusRepository;
            this.userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();
            if (!user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var entity = await _satusRepository.GetStatusByIdAsync(request.StatusId);
            if (entity == null)
                return new BaseCommandResponse(new InfoMessage("The requested status id was not found in database"));

            bool isBeingUsed = await _satusRepository.IsBeingUsed(entity);
            if (isBeingUsed)
                return new BaseCommandResponse(new InfoMessage("Cannot be delete because some workers is using that state"));

            await _satusRepository.DeleteStatusAsync(entity);
            return new BaseCommandResponse($"Status {entity.Name} deleted succesfully");
        }

    }
}
