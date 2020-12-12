using MediatR;
using System;

namespace WorkerTracking.Core.Commands
{
    public class UpdateWorkerStatusCommand : IRequest<string>
    {
        public Guid WorkerId { get; set; }
        public int StatusId { get; set; }
    }
}
