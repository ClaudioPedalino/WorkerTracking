using MediatR;
using System;
using System.Collections.Generic;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers.Models;

namespace WorkerTracking.Core.Queries
{
    public class GetAllWorkersQuery : PaginationFilter, IRequest<Tuple<IEnumerable<WorkerModel>, int>>
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? NameToSearch { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public int? StatusId { get; set; }
        public Guid? TeamId { get; set; }
        public int? RoleId { get; set; }
    }
}
