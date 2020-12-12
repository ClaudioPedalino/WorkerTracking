using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, string>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<string> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var entity = await _roleRepository.GetRoleByIdAsync(command.RoleId);
            if (entity == null) return "The requestes Role id was not found in database";

            bool isBeingUsed = await _roleRepository.IsBeingUsed(entity);
            if (isBeingUsed) return "Cannot be delete because some workers is using that state";

            await _roleRepository.DeleteRoleAsync(entity);
            return "Role deleted succesfully";
        }

    }
}
