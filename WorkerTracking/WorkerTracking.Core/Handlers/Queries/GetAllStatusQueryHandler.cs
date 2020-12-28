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
    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, List<StatusModel>>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IWorkerRepository _workerRepository;

        public GetAllStatusQueryHandler(IStatusRepository statusRepository,
                                        IWorkerRepository workerRepository)
        {
            _statusRepository = statusRepository ?? throw new System.ArgumentNullException(nameof(statusRepository));
            _workerRepository = workerRepository ?? throw new System.ArgumentNullException(nameof(workerRepository));
        }

        public async Task<List<StatusModel>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
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
