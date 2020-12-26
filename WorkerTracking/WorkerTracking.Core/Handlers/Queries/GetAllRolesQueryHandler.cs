using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleModel>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleModel>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roleDb = await _roleRepository.GetAllRolesAsync();

            var result = roleDb.Select(x => new RoleModel(roleId: x.RoleId,
                                                          roleName: x.Name,
                                                          abbreviation: x.Abbreviation))
                               .ToList();

            return result;
        }

    }
}
