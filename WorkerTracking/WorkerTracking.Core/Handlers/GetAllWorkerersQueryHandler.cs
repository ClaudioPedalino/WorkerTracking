using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;
using WorkerTracking.Data.Repositories;
using WorkerTracking.Entities;

namespace WorkerTracking.Core.Handlers
{
    public class GetAllWorkerersQueryHandler : IRequestHandler<GetAllWorkerersQuery, IEnumerable<WorkerModel>>
    {
        private readonly IWorkerRepository _workerRepository;

        public GetAllWorkerersQueryHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<IEnumerable<WorkerModel>> Handle(GetAllWorkerersQuery request, CancellationToken cancellationToken)
        {
            var response = new List<WorkerModel>();
            var workersDb = await _workerRepository.GetAllWorkersAsync();

            var pepe01 = request.PageNumber;
            var pepe02 = request.PageSize;

            foreach (var w in workersDb)
            {
                var worker = new WorkerModel()
                {
                    WorkerId = w.WorkerId,
                    FirstName = w.FirstName,
                    LastName = w.LastName,
                    Email = w.Email,
                    Birthday = w.Birthday,
                    PhotoUrl = w.PhotoUrl,
                    StatusName = w.Status.Name,
                    Role = w.Role.Name,
                    IsBirthdayToday = VerifyBirthday(DateTime.Now, w.Birthday), ///logica de sábados y domingos
                    Teams = w.WorkersByTeamId.Select(x => x.Team.Name).ToList()
                };
                response.Add(worker);
            }

            var pepito = response.Where(x => x.IsBirthdayToday).Count();
            return response.Take(20);
        }


        //TODO: <refactor>
        private bool VerifyBirthday(DateTime date, DateTime birthday)
        {
            if (date.Date.ToString("dd-MM").Equals(birthday.Date.ToString("dd-MM")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
