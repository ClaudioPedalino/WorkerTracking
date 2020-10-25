using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers.Models;

namespace WorkerTracking.Core.Queries
{
    public class GetWorkerByIdQuery : IRequest<WorkerModel>
    {
        public Guid WorkerId { get; set; }
    }
}
