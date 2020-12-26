using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, BaseCommandResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var newRole = new Role(request.RoleName, request.RoleAbbreviation.ToUpper());

            await _roleRepository.CreateRoleAsync(newRole);

            return new BaseCommandResponse($"Role {newRole.Name} created succesfully");
        }
    }
}
