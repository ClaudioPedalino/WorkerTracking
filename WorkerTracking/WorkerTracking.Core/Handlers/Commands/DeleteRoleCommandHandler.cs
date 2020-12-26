using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, BaseCommandResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _roleRepository.GetRoleByIdAsync(request.RoleId);
            if (entity == null)
                return new BaseCommandResponse("The requested role was not found in database");

            bool isBeingUsed = await _roleRepository.IsBeingUsed(entity);
            if (isBeingUsed)
                return new BaseCommandResponse("Cannot be delete because some workers is using that state");

            await _roleRepository.DeleteRoleAsync(entity);
            return new BaseCommandResponse("Role deleted succesfully");
        }

    }
}
