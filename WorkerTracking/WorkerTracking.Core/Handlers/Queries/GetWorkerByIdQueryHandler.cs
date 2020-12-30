using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Exceptions;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class GetWorkerByIdQueryHandler : IRequestHandler<GetWorkerByIdQuery, WorkerModel>
    {
        private readonly IUserStore<User> userStore;
        private readonly IWorkerRepository _workerRepository;

        public GetWorkerByIdQueryHandler(IWorkerRepository workerRepository, IUserStore<User> userStore)
        {
            _workerRepository = workerRepository;
            this.userStore = userStore;
        }

        public async Task<WorkerModel> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userStore.FindByIdAsync(request.GetUser(), cancellationToken);
            if (user == null) throw new UserDoesNotExistException();

            var workerDb = await _workerRepository.GetWorkerByIdAsync(request.WorkerId);

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
                LastModificationTime = workerDb.GetLastModificationTime(),
                //IsBirthdayToday = VerifyBirthday(DateTime.Now, workerDb.Birthday),
                //Teams = workerDb.WorkersByTeamId.Select(x => new TeamModel(x.Team.TeamId, x.Team.Name)).ToList()
            };

            return response;
        }
    }
}
