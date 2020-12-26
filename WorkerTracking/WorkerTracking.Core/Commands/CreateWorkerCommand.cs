using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using WorkerTracking.Core.Commands.Base;

namespace WorkerTracking.Core.Commands
{
    public class CreateWorkerCommand : IRequest<BaseCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string PhotoUrl { get; set; }
        public int RoleId { get; set; }
        public List<Guid> TeamId { get; set; }

    }

    public class CreateWorkerCommandValidator : AbstractValidator<CreateWorkerCommand>
    {
        public CreateWorkerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(30).WithMessage("{PropertyName} can not be more than 30 characters");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(30).WithMessage("{PropertyName} can not be more than 30 characters");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(30).WithMessage("{PropertyName} can not be more than 30 characters");

            RuleFor(x => x.RoleId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");

        }
    }
}
