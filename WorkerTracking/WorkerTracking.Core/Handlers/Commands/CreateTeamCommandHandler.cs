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
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, BaseCommandResponse>
    {
        private readonly IUserStore<User> userStore;
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository, IUserStore<User> userStore)
        {
            _teamRepository = teamRepository;
            this.userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();
            if (!user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var newTeam = new Team(name: request.Name);

            await _teamRepository.CreateTeamAsync(newTeam);

            return new BaseCommandResponse($"Team {newTeam.Name} created succesfully");
        }
    }
}
