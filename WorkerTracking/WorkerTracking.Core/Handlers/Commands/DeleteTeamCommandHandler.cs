using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Exceptions;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, BaseCommandResponse>
    {
        private readonly IUserStore<User> userStore;
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository, IUserStore<User> userStore)
        {
            _teamRepository = teamRepository;
            this.userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();
            if (!user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var teamDb = await _teamRepository.GetTeamByIdAsync(request.TeamId);
            if (teamDb == null)
                return new BaseCommandResponse(new InfoMessage("The requested team id was not found in database"));

            bool isBeingUsed = await _teamRepository.IsBeingUsed(teamDb.TeamId);
            if (isBeingUsed)
                return new BaseCommandResponse(new InfoMessage("Cannot delete because some workers is using that team"));

            await _teamRepository.DeleteTeamAsync(teamDb);
            return new BaseCommandResponse($"Team {teamDb.Name} deleted succesfully");
        }

    }
}