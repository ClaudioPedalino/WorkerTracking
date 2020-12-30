using MediatR;
using System;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Queries
{
    public class GetWorkerByIdQuery : LoggedRequest, IRequest<WorkerModel>
    {
        public Guid WorkerId { get; set; }
    }
}
