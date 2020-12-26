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

        public GetAllStatusQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<List<StatusModel>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            var statusDb = await _statusRepository.GetAllStatusAsync();
            var statusList = new List<StatusModel>();

            if (HasResults(statusDb))
            {
                statusList.AddRange(
                    statusDb.Select(x => new StatusModel(
                                            statusId: x.StatusId,
                                            statusName: x.Name)));
            }

            return statusList;
        }

        private static bool HasResults(IEnumerable<Entities.Status> statusDb)
            => statusDb != null
            && statusDb.Any();
    }
}
