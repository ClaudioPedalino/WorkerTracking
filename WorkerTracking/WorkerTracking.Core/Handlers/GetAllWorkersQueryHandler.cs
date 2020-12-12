using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetAllWorkersQueryHandler : IRequestHandler<GetAllWorkersQuery, Tuple<IEnumerable<WorkerModel>, int>>
    {
        private readonly IWorkerRepository _workerRepository;

        public GetAllWorkersQueryHandler(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<Tuple<IEnumerable<WorkerModel>, int>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
        {
            var workersDb = await _workerRepository.GetAllWorkersAsync();

            var response = CreateResponse(workersDb);
            var hayCumpleañito = response.Where(x => x.IsBirthdayToday).Count();

            if (NeedToFilter(request))
                response = FilterResults(request, response);

            return new Tuple<IEnumerable<WorkerModel>, int>(response
                .Skip(PaginationHelper.GetSkipRows(request))
                .Take(request.PageSize),
                workersDb.Count());
        }

        private static bool NeedToFilter(GetAllWorkersQuery request)
            => request.StatusId.HasValue
            || request.RoleId.HasValue
            || request.TeamId.HasValue
            || !string.IsNullOrWhiteSpace(request.NameToSearch);

        private List<WorkerModel> FilterResults(GetAllWorkersQuery request, List<WorkerModel> response)
        {
            //TODO: Refactor Create Object filter and move filter to repository
            if (request.StatusId.HasValue)
                response = response.Where(x => x.StatusId == request.StatusId).ToList();

            if (request.RoleId.HasValue)
                response = response.Where(x => x.RoleId == request.RoleId).ToList();

            if (request.TeamId.HasValue)
            {
                //var pepe = response.SelectMany(x => x.Teams.Where(y => y.TeamId == request.TeamId.Value));
                //var tete = response.ForEach(x => x.Teams.S(y => y.TeamId == request.TeamId));
            }
            if (!string.IsNullOrWhiteSpace(request.NameToSearch))
            {
                response = response.Where(x => EF.Functions.Like(x.FirstName, $"%{request.NameToSearch}%")
                                            || EF.Functions.Like(x.LastName, $"%{request.NameToSearch}%"))
                                   .ToList();
            }

            return response;
        }

        private List<WorkerModel> CreateResponse(IEnumerable<Entities.Worker> workersDb)
        {
            var response = new List<WorkerModel>();
            response.AddRange(
                workersDb.Select(w => new WorkerModel()
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
                    Teams = w.WorkersByTeamId.Select(x => new TeamModel(x.Team.TeamId, x.Team.Name)).ToList()
                }));

            return response;
        }


        private bool VerifyBirthday(DateTime date, DateTime birthday)
            => date.Date.ToString("dd-MM")
            .Equals(birthday.Date.ToString("dd-MM"));
    }
}
