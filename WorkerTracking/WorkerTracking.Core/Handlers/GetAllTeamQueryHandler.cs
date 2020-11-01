using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class GetAllTeamQueryHandler : IRequestHandler<GetAllTeamQuery, List<TeamModel>>
    {
        private readonly ITeamRepository _teamRepository;

        public GetAllTeamQueryHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<TeamModel>> Handle(GetAllTeamQuery request, CancellationToken cancellationToken)
        {
            var teamsDb = await _teamRepository.GetAllWorkersAsync();
            var response = new List<TeamModel>();

            response.AddRange(
                teamsDb.Select(x =>
                    new TeamModel(x.TeamId, x.Name)));

            return response;
        }
    }
}
