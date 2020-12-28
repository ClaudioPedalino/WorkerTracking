using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkerTracking.Core.Commands;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Data.Interfaces;

namespace WorkerTracking.Core.Handlers
{
    public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, BaseCommandResponse>
    {
        private readonly IStatusRepository _satusRepository;

        public DeleteStatusCommandHandler(IStatusRepository satusRepository)
        {
            _satusRepository = satusRepository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _satusRepository.GetStatusByIdAsync(request.StatusId);
            if (entity == null)
                return new BaseCommandResponse("The requested status id was not found in database");

            bool isBeingUsed = await _satusRepository.IsBeingUsed(entity);
            if (isBeingUsed)
                return new BaseCommandResponse("Cannot be delete because some workers is using that state");

            await _satusRepository.DeleteStatusAsync(entity);
            return new BaseCommandResponse($"Status {entity.Name} deleted succesfully");
        }

    }
}
