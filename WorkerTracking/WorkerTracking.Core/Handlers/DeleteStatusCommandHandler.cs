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
    public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, string>
    {
        private readonly IStatusRepository _satusRepository;

        public DeleteStatusCommandHandler(IStatusRepository satusRepository)
        {
            _satusRepository = satusRepository;
        }

        public async Task<string> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
        {
            var entity = await _satusRepository.GetStatusByIdAsync(command.StatusId);
            if (entity == null) return "The requestes status id was not found in database"; 
            
            bool isBeingUsed = await _satusRepository.IsBeingUsed(entity);
            if (isBeingUsed) return "Cannot be delete because some workers is using that state";

            await _satusRepository.DeleteStatusAsync(entity);
            return "Status deleted succesfully";
        }

    }
}
