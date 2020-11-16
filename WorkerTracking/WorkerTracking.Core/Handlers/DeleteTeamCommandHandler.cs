using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, string>
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<string> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
        {
            var entity = await _teamRepository.GetTeamByIdAsync(command.TeamId);
            if (entity == null) return "The requestes status id was not found in database";

            //bool isBeingUsed = await _teamRepository.IsBeingUsed(entity);
            //if (isBeingUsed) return "Cannot be delete because some workers is using that state";

            await _teamRepository.DeleteTeamAsync(entity);
            return "Team deleted succesfully";
        }

    }
}