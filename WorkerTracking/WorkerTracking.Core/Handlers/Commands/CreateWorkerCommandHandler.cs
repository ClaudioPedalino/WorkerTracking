using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
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
        private readonly IStatusRepository _statusRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository, IWorkersByTeamRepository workersByTeamRepository, ITeamRepository teamRepository, IUserStore<User> userStore, IStatusRepository statusRepository, IRoleRepository roleRepository)
        {
            _workerRepository = workerRepository;
            _workersByTeamRepository = workersByTeamRepository;
            _teamRepository = teamRepository;
            this.userStore = userStore;
            _statusRepository = statusRepository;
            _roleRepository = roleRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();
            if (!user.IsAdmin) throw new UnauthorizedAccessException("User does not have permission for that action");

            var roleToAdd = await _roleRepository.GetRoleByIdAsync(request.RoleId);
            var roleId = roleToAdd != null ? roleToAdd.RoleId : (int)RolesEnum.NotAssigned;

            var validUser = await userStore.FindByNameAsync(request.Email.ToUpper(), cancellationToken);
            if (validUser != null)
                return new BaseCommandResponse(new InfoMessage("There is already an user registered with that email"));

            //var validRole = RolesEnum.IsDefined(typeof(RolesEnum), request.RoleId);
            //if (!validRole)
            //    return new BaseCommandResponse(new InfoMessage("That role does not exist"));
            //var defaultStatus = await _statusRepository.GetDefaultStatus();

            var newWorker = new Worker(request.FirstName,
                                       request.LastName,
                                       request.Email,
                                       request.Birthday,
                                       !string.IsNullOrWhiteSpace(request.PhotoUrl)
                                        ? request.PhotoUrl
                                        : "",
                                       (int)StatusEnum.Inactive,
                                       roleId,
                                       DateTime.Now);

            //newWorker.Status = defaultStatus;
            //newWorker.Role = roleToAdd;
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