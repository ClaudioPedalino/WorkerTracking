using MediatR;

namespace WorkerTracking.Core.Commands
{
    public class DeleteRoleCommand : IRequest<string>
    {
        public int RoleId { get; set; }
    }
}
