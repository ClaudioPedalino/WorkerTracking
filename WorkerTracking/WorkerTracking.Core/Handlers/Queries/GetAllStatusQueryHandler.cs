using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
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
    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, List<StatusModel>>
    {
        private readonly IUserStore<User> userStore;
        private readonly IStatusRepository _statusRepository;
        private readonly IWorkerRepository _workerRepository;

        public GetAllStatusQueryHandler(IStatusRepository statusRepository,
                                        IWorkerRepository workerRepository, IUserStore<User> userStore)
        {
            _statusRepository = statusRepository ?? throw new System.ArgumentNullException(nameof(statusRepository));
            _workerRepository = workerRepository ?? throw new System.ArgumentNullException(nameof(workerRepository));
            this.userStore = userStore;
        }

        public async Task<List<StatusModel>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new ArgumentNullException("User does not exists");
            if (user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var statusDb = await _statusRepository.GetAllStatusAsync();
            var workersDb = await _workerRepository.GetAllWorkersAsync();

            var groupByStatusId = workersDb
                                    .GroupBy(x => x.Status)
                                    .Select(w => new StatusModel(w.Key.StatusId, w.Count())
                                    );

            var response = statusDb.Select(x =>
                                    new StatusModel(statusId: x.StatusId,
                                                    statusName: x.Name)
                                    {
                                        TotalWorkers = groupByStatusId.Any(y => y.StatusId == x.StatusId)
                                                        ? groupByStatusId.Where(y => y.StatusId == x.StatusId).Select(y => y.TotalWorkers).FirstOrDefault()
                                                        : 0
                                    })
                                    .ToList();

            return response;
        }

    }
}
