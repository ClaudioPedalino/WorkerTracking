using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Queries.Workers
{
    public class GetAllWorkerersQuery : IRequest<IEnumerable<Worker>>
    {

    }
}
