﻿using FluentValidation;
using MediatR;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Commands
{
    public class CreateTeamCommand : LoggedRequest, IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
    }

    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(30).WithMessage("{PropertyName} can not be more than 30 characters");

        }
    }
}
