﻿using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class GetWorkerByIdQueryHandler : IRequestHandler<GetWorkerByIdQuery, WorkerModel>
    {
        private readonly IWorkerRepository _workerRepository;

        public GetWorkerByIdQueryHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<WorkerModel> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            var workerDb = await _workerRepository.GetWorkerByIdAsync(request.WorkerId);

            var response = new WorkerModel()
            {
                WorkerId = workerDb.WorkerId,
                FirstName = workerDb.FirstName,
                LastName = workerDb.LastName,
                Email = workerDb.Email,
                Birthday = workerDb.Birthday,
                PhotoUrl = workerDb.PhotoUrl,
                StatusName = workerDb.Status.Name,
                StatusId = workerDb.Status.StatusId,
                Role = workerDb.Role.Name,
                RoleId = workerDb.Role.RoleId,
                LastModificationTime = workerDb.LastModificationTime,
                //TODO:
                //IsBirthdayToday = VerifyBirthday(DateTime.Now, workerDb.Birthday), ///logica de sábados y domingos
                Teams = workerDb.WorkersByTeamId.Select(x => new TeamModel(x.Team.TeamId, x.Team.Name)).ToList()
            };

            return response;
        }
    }
}
