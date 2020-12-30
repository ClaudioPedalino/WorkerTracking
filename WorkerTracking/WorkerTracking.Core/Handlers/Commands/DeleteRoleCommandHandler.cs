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
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, BaseCommandResponse>
    {
        private readonly IUserStore<User> userStore;
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository, IUserStore<User> userStore)
        {
            _roleRepository = roleRepository;
            this.userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new ArgumentNullException("User does not exists");
            if (user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var entity = await _roleRepository.GetRoleByIdAsync(request.RoleId);
            if (entity == null)
                return new BaseCommandResponse("The requested role was not found in database");

            bool isBeingUsed = await _roleRepository.IsBeingUsed(entity);
            if (isBeingUsed)
                return new BaseCommandResponse("Cannot be delete because some workers is using that role");

            await _roleRepository.DeleteRoleAsync(entity);
            return new BaseCommandResponse($"Role {entity.Name} deleted succesfully");
        }

    }
}
