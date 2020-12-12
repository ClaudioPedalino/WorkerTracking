using MediatR;
using System;
using WorkerTracking.Core.Handlers.Models;

namespace WorkerTracking.Core.Queries
{
    public class GetWorkerByIdQuery : IRequest<WorkerModel>
    {
        public Guid WorkerId { get; set; }
    }
}
