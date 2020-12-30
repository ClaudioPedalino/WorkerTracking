using FluentValidation;
using MediatR;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Commands
{
    public class CreateRoleCommand : LoggedRequest, IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(30).WithMessage("{PropertyName} can not be more than 30 characters");

            RuleFor(x => x.Abbreviation)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(3).WithMessage("{PropertyName} can not be more than 3 characters");
        }
    }
}
