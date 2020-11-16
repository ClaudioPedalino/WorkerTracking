using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, string>
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IWorkersByTeamRepository _workersByTeamRepository;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository, IWorkersByTeamRepository workersByTeamRepository, ITeamRepository teamRepository)
        {
            _workerRepository = workerRepository;
            _workersByTeamRepository = workersByTeamRepository;
            _teamRepository = teamRepository;
        }

        public async Task<string> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var newWorker = new Worker()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Birthday = request.Birthday,
                PhotoUrl = request.PhotoUrl,
                StatusId = request.StatusId,
                RoleId = request.RoleId,
                LastModificationTime = DateTime.Now,
                IsActive = request.IsActive
            };

            await _workerRepository.CreateWorkerAsync(newWorker);

            if (request.TeamId.Length > 0)
            {
                foreach (var teamId in request.TeamId)
                {
                    var team = await _teamRepository.GetTeamByIdAsync(teamId);

                    var newWorkerByTeam = new WorkersByTeam(newWorker.WorkerId, teamId);

                    await _workersByTeamRepository.CreateWorkerByTeam(newWorkerByTeam);
                }

            }

            return "Worker created succesfully";
        }
    }
}