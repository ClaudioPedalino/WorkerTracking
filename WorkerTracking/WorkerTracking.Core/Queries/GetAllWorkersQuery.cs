using MediatR;
using System;
using System.Collections.Generic;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers.Models;

namespace WorkerTracking.Core.Queries
{
    public class GetAllWorkersQuery : PaginationFilter, IRequest<Tuple<IEnumerable<WorkerModel>, int>>
    {
        public string? NameToSearch { get; set; }
        public int? StatusId { get; set; }
        public Guid? TeamId { get; set; }
        public int? RoleId { get; set; }
    }
}
