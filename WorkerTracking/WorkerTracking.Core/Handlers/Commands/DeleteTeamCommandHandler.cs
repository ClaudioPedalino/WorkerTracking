using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, BaseCommandResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var teamDb = await _teamRepository.GetTeamByIdAsync(request.TeamId);
            if (teamDb == null)
                return new BaseCommandResponse("The requested team id was not found in database");

            bool isBeingUsed = await _teamRepository.IsBeingUsed(teamDb.TeamId);
            if (isBeingUsed)
                return new BaseCommandResponse("Cannot delete because some workers is using that team");

            await _teamRepository.DeleteTeamAsync(teamDb);
            return new BaseCommandResponse($"Team {teamDb.Name} deleted succesfully");
        }

    }
}