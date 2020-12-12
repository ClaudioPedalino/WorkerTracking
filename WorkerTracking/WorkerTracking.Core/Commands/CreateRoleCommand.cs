using MediatR;

namespace WorkerTracking.Core.Commands
{
    public class CreateRoleCommand : IRequest<string>
    {
        public string RoleName { get; set; }
        public string RoleAbbreviation { get; set; }
    }
}
