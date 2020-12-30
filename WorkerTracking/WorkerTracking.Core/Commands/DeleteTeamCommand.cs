using FluentValidation;
using MediatR;
using System;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Commands
{
    public class DeleteTeamCommand : LoggedRequest, IRequest<BaseCommandResponse>
    {
        public Guid TeamId { get; set; }
    }

    public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        public DeleteTeamCommandValidator()
        {
            RuleFor(x => x.TeamId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
        }
    }
}
