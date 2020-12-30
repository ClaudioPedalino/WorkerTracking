using MediatR;
using System.Collections.Generic;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Queries
{
    public class GetAllRolesQuery : LoggedRequest, IRequest<List<RoleModel>>
    {
    }
}
