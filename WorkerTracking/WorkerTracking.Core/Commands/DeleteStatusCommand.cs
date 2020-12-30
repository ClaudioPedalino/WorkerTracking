using FluentValidation;
using MediatR;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Commands
{
    public class DeleteStatusCommand : LoggedRequest, IRequest<BaseCommandResponse>
    {
        public int StatusId { get; set; }
    }

    public class DeleteStatusCommandValidator : AbstractValidator<DeleteStatusCommand>
    {
        public DeleteStatusCommandValidator()
        {
            RuleFor(x => x.StatusId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
        }
    }
}
