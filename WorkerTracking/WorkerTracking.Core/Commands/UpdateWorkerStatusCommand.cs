using FluentValidation;
using MediatR;
using System;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Commands
{
    public class UpdateWorkerStatusCommand : LoggedRequest, IRequest<BaseCommandResponse>
    {
        public Guid WorkerId { get; set; }
        public int StatusId { get; set; }
    }

    public class UpdateWorkerStatusCommandValidator : AbstractValidator<UpdateWorkerStatusCommand>
    {
        public UpdateWorkerStatusCommandValidator()
        {
            RuleFor(x => x.WorkerId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");

            RuleFor(x => x.StatusId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
        }
    }
}
