using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, BaseCommandResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var newTeam = new Team(name: request.TeamName);

            await _teamRepository.CreateTeamAsync(newTeam);

            return new BaseCommandResponse($"Team {newTeam.Name} created succesfully");
        }
    }
}
