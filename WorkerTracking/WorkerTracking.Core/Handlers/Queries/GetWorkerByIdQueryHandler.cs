using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
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
    public class GetWorkerByIdQueryHandler : IRequestHandler<GetWorkerMyInfo, WorkerModel>
    {
        private readonly IUserStore<User> userStore;
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkersByTeamRepository _workersByTeamRepository;

        public GetWorkerByIdQueryHandler(IWorkerRepository workerRepository, IUserStore<User> userStore, IWorkersByTeamRepository workersByTeamRepository)
        {
            _workerRepository = workerRepository;
            this.userStore = userStore;
            _workersByTeamRepository = workersByTeamRepository;
        }

        public async Task<WorkerModel> Handle(GetWorkerMyInfo request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();

            var workerDb = await _workerRepository.GetWorkerByUserNameAsync(user.Email);
            if (workerDb == null) return new WorkerModel();

            var workderDbteamList = await _workersByTeamRepository.GetWorkerTeamInfo(workerDb.WorkerId);

            var response = new WorkerModel()
            {
                WorkerId = workerDb.WorkerId,
                FirstName = workerDb.FirstName,
                LastName = workerDb.LastName,
                Email = workerDb.Email,
                Birthday = workerDb.Birthday,
                PhotoUrl = workerDb.PhotoUrl,
                Status = workerDb.Status.Name,
                StatusId = workerDb.Status.StatusId,
                Role = workerDb.Role.Name,
                RoleId = workerDb.Role.RoleId,
                LastModificationTime = workerDb.LastModificationTime,
                IsBirthdayToday = VerifyBirthday(DateTime.Now, workerDb.Birthday),
                Teams = workderDbteamList != null
                    ? workderDbteamList.Select(x => new TeamModel(x.Team.TeamId, x.Team.Name)).ToList()
                    : new List<TeamModel>()
            };

            return response;
        }
        private bool VerifyBirthday(DateTime date, DateTime birthday)
                => date.Date.ToString("dd-MM")
                .Equals(birthday.Date.ToString("dd-MM"));
    }
}
