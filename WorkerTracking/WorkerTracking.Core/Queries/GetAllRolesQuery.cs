using MediatR;
using System.Collections.Generic;
using WorkerTracking.Core.Handlers.Models;

namespace WorkerTracking.Core.Queries
{
    public class GetAllRolesQuery : IRequest<List<RoleModel>>
    {
    }
}
