﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Core.Common;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Queries
{
    public class GetAllWorkerersQuery : PaginationFilter, IRequest<IEnumerable<WorkerModel>>
    {
        public int? StatusId { get; set; }
        public Guid? TeamId { get; set; }
        public int? RoleId { get; set; }
    }
}