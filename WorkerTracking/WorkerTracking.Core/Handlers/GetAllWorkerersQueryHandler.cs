using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Queries.Workers;
using WorkerTracking.Data.Repositories;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class GetAllWorkerersQueryHandler : IRequestHandler<GetAllWorkerersQuery, IEnumerable<Worker>>
    {
        private readonly IWorkerRepository _workerRepository;

        public GetAllWorkerersQueryHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<IEnumerable<Worker>> Handle(GetAllWorkerersQuery request, CancellationToken cancellationToken)
        {
            var response = await _workerRepository.GetAllWorkersAsync();


            return response;
        }
    }
}
