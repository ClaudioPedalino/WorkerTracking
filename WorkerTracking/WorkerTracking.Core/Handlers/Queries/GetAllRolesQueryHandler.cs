using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleModel>>
    {
        private readonly IUserStore<User> userStore;
        private readonly IRoleRepository _roleRepository;
        private readonly IWorkerRepository _workerRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository,
                                       IWorkerRepository workerRepository, IUserStore<User> userStore)
        {
            _roleRepository = roleRepository ?? throw new System.ArgumentNullException(nameof(roleRepository));
            _workerRepository = workerRepository ?? throw new System.ArgumentNullException(nameof(workerRepository));
            this.userStore = userStore;
        }

        public async Task<List<RoleModel>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new Exceptions.UserDoesNotExistException();

            var roleDb = await _roleRepository.GetAllRolesAsync();
            var workersDb = await _workerRepository.GetAllWorkersAsync();


            var groupByRoleId = workersDb
                                    .GroupBy(x => x.Role)
                                    .Select(w => new RoleModel(w.Key.RoleId, w.Count())
                                    );

            var response = roleDb.Select(x =>
                                    new RoleModel(roleId: x.RoleId,
                                                  roleName: x.Name,
                                                  abbreviation: x.Abbreviation)
                                    {
                                        TotalWorkers = groupByRoleId.Any(y => y.RoleId == x.RoleId)
                                                        ? groupByRoleId.Where(y => y.RoleId == x.RoleId).Select(y => y.TotalWorkers).FirstOrDefault()
                                                        : 0
                                    })
                               .ToList();

            return response;
        }

    }
}
