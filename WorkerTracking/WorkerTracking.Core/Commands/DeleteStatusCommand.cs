using MediatR;

namespace WorkerTracking.Core.Commands
{
    public class DeleteStatusCommand : IRequest<string>
    {
        public int StatusId { get; set; }
    }
}
