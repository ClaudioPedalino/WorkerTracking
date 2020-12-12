using MediatR;

namespace WorkerTracking.Core.Commands
{
    public class CreateStatusCommand : IRequest<string>
    {
        public string StatusName { get; set; }
    }
}
