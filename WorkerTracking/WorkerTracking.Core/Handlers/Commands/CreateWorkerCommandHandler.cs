using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Enums;
using WorkerTracking.Core.Exceptions;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, BaseCommandResponse>
    {
        private readonly IUserStore<User> userStore;
        private readonly IWorkerRepository _workerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IWorkersByTeamRepository _workersByTeamRepository;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository, IWorkersByTeamRepository workersByTeamRepository, ITeamRepository teamRepository, IUserStore<User> userStore)
        {
            _workerRepository = workerRepository;
            _workersByTeamRepository = workersByTeamRepository;
            _teamRepository = teamRepository;
            this.userStore = userStore;
        }

        public async Task<BaseCommandResponse> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();
            if (!user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var validRole = RolesEnum.IsDefined(typeof(RolesEnum), request.RoleId);
            if (!validRole)
                return new BaseCommandResponse(new InfoMessage("That role does not exist"));

            var newWorker = new Worker(request.FirstName,
                                       request.LastName,
                                       request.Email,
                                       request.Birthday,
                                       request.PhotoUrl,
                                       (int)StatusEnum.Inactive,
                                       request.RoleId,
                                       DateTime.Now);

            await _workerRepository.CreateWorkerAsync(newWorker);

            string teamsName = "";
            if (request.TeamId.Count > 0)
            {
                foreach (var teamId in request.TeamId)
                {
                    var team = await _teamRepository.GetTeamByIdAsync(teamId);
                    if (team == null)
                        return new BaseCommandResponse(new InfoMessage("That team does not exist"));

                    var newWorkerByTeam = new WorkersByTeam(newWorker.WorkerId, team.TeamId);
                    teamsName += String.Concat(team.Name, ", ");
                    await _workersByTeamRepository.CreateWorkerByTeam(newWorkerByTeam);
                }
            }

            return new BaseCommandResponse($"Worker {newWorker.FirstName} {newWorker.LastName} created succesfully into teams: {teamsName.Remove(teamsName.Length - 2)}");
        }
    }
}