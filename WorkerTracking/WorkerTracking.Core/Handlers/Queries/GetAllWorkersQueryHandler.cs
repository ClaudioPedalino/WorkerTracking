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
        private readonly IWorkersByTeamRepository _workersByTeamRepository;

        public GetAllWorkersQueryHandler(IWorkersByTeamRepository workersByTeamRepository)
        {
            _workersByTeamRepository = workersByTeamRepository;
        }

        public async Task<Tuple<IEnumerable<WorkerModel>, int>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
        {
            var workersByTeamDb = await _workersByTeamRepository.GetAllWorkersWithTeamInfo();

            var response = workersByTeamDb
                                .Select(x => x.Worker)
                                .Distinct()
                                    .Select(item => new WorkerModel
                                    {
                                        WorkerId = item.WorkerId,
                                        FirstName = item.FirstName,
                                        LastName = item.LastName,
                                        Email = item.Email,
                                        Birthday = item.Birthday,
                                        PhotoUrl = item.PhotoUrl,
                                        StatusId = item.StatusId,
                                        StatusName = item.Status.Name,
                                        RoleId = item.RoleId,
                                        Role = item.Role.Name,
                                        LastModificationTime = item.LastModificationTime,
                                        IsBirthdayToday = VerifyBirthday(DateTime.Now, item.Birthday),
                                        Teams = workersByTeamDb.Where(x => x.WorkerId == item.WorkerId)
                                                               .Select(x => new TeamModel(x.Team.TeamId, x.Team.Name))
                                                               .ToList()
                                    })
                                .ToList();


            if (NeedToFilter(request))
                response = FilterResults(request, response);

            return new Tuple<IEnumerable<WorkerModel>, int>(response
                .Skip(PaginationHelper.GetSkipRows(request))
                .Take(request.PageSize),
                response.Count);
        }

        private static bool NeedToFilter(GetAllWorkersQuery request)
            => request.StatusId.HasValue
            || request.RoleId.HasValue
            || request.TeamId.HasValue
            || !string.IsNullOrWhiteSpace(request.NameToSearch);

        private List<WorkerModel> FilterResults(GetAllWorkersQuery request, List<WorkerModel> response)
        {
            if (request.StatusId.HasValue)
                response = response.Where(x => x.StatusId == request.StatusId).ToList();

            if (request.RoleId.HasValue)
                response = response.Where(x => x.RoleId == request.RoleId).ToList();

            if (request.TeamId.HasValue)
            {
                response = response.Where(x => x.Teams.Any(y => y.TeamId == request.TeamId.Value)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(request.NameToSearch))
            {
                response = response.Where(x => EF.Functions.Like(x.FirstName, $"%{request.NameToSearch}%")
                                            || EF.Functions.Like(x.LastName, $"%{request.NameToSearch}%"))
                                   .ToList();
            }

            return response;
        }


        private bool VerifyBirthday(DateTime date, DateTime birthday)
            => date.Date.ToString("dd-MM")
            .Equals(birthday.Date.ToString("dd-MM"));
    }
}
