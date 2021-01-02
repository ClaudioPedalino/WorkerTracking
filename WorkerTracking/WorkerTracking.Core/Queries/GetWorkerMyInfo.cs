using MediatR;
using System;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Queries
{
    public class GetWorkerMyInfo : LoggedRequest, IRequest<WorkerModel>
    {
    }
}
