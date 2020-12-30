using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Exceptions;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class GetAllTeamQueryHandler : IRequestHandler<GetAllTeamQuery, List<TeamModel>>
    {
        private readonly IUserStore<User> userStore;
        private readonly ITeamRepository _teamRepository;
        private readonly IWorkersByTeamRepository _workersByTeamRepository;

        public GetAllTeamQueryHandler(ITeamRepository teamRepository,
                                      IWorkersByTeamRepository workersByTeamRepository,
                                      IUserStore<User> userStore)
        {
            _teamRepository = teamRepository ?? throw new System.ArgumentNullException(nameof(teamRepository));
            _workersByTeamRepository = workersByTeamRepository ?? throw new System.ArgumentNullException(nameof(workersByTeamRepository));
            this.userStore = userStore;
        }

        public async Task<List<TeamModel>> Handle(GetAllTeamQuery request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();

            var teamsDb = await _teamRepository.GetAllTeamsAsync();
            var teamsByWorkerDb = await _workersByTeamRepository.GetTotalWorkersByTeam();

            var response = new List<TeamModel>();

            var groupByTeamId = teamsByWorkerDb
                                    .GroupBy(x => x.TeamId)
                                    .Select(w => new TeamModel(w.Key, w.Count())
                                    );

            response.AddRange(
                teamsDb.Select(x =>
                    new TeamModel(x.TeamId, x.Name)
                    {
                        TotalWorkers = groupByTeamId.Any(y => y.TeamId == x.TeamId)
                        ? groupByTeamId.Where(y => y.TeamId == x.TeamId).Select(y => y.TotalWorkers).FirstOrDefault()
                        : 0
                    })
                );

            return response;
        }
    }
}
