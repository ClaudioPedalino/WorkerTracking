using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Handlers.Models;
using WorkerTracking.Core.Helpers;
using WorkerTracking.Core.Queries;
using WorkerTracking.Data.Interfaces;

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
            var workersDb = await _workerRepository.GetAllWorkersAsync();

            var response = CreateResponse(workersDb);
            var hayCumpleañito = response.Where(x => x.IsBirthdayToday).Count();

            if (NeedToFilter(request))
                response = FilterResults(request, response);

            return response
                .Skip(PaginationHelper.GetSkipRows(request))
                .Take(request.PageSize);
        }

        private static bool NeedToFilter(GetAllWorkerersQuery request) 
            => request.StatusId.HasValue 
            || request.RoleId.HasValue 
            || request.TeamId.HasValue;

        private List<WorkerModel> FilterResults(GetAllWorkerersQuery request, List<WorkerModel> response)
        {
            if (request.StatusId.HasValue)
                response = response.Where(x => x.StatusId == request.StatusId).ToList();

            if (request.RoleId.HasValue)
                response = response.Where(x => x.RoleId == request.RoleId).ToList();

            if (request.TeamId.HasValue)
            {
                //var pepe = response.SelectMany(x => x.Teams.Where(y => y.TeamId == request.TeamId.Value));
                //var tete = response.ForEach(x => x.Teams.Where(x => x.TeamId == request.TeamId));
            }

            return response;
        }

        private List<WorkerModel> CreateResponse(IEnumerable<Entities.Worker> workersDb)
        {
            //TODO: <refactor>
            var response = new List<WorkerModel>();
            
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
                    StatusId = w.Status.StatusId,
                    Role = w.Role.Name,
                    RoleId = w.Role.RoleId,
                    LastModificationTime = w.LastModificationTime,
                    IsBirthdayToday = VerifyBirthday(DateTime.Now, w.Birthday), ///logica de sábados y domingos
                    Teams = w.WorkersByTeamId.Select(x => new TeamsModel() {TeamId = x.Team.TeamId, Name = x.Team.Name }).ToList()
                };
                response.Add(worker);
            }

            return response;
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
