﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Core.Handlers.Models;

namespace WorkerTracking.Core.Queries
{
    public class GetAllRolesQuery : IRequest<List<RoleModel>>
    {
    }
}