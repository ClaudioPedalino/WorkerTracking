using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerTracking.Core.Commands
{
    public class UpdateWorkerStatusCommand : IRequest<string>
    {
        public Guid WorkerId { get; set; }
        public int StatusId { get; set; }
    }
}
