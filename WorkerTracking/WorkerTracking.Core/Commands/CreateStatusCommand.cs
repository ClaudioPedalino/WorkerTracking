using FluentValidation;
using MediatR;
using WorkerTracking.Core.Commands.Base;

namespace WorkerTracking.Core.Commands
{
    public class CreateStatusCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
    }

    public class CreateStatusCommandValidator : AbstractValidator<CreateStatusCommand>
    {
        public CreateStatusCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .MaximumLength(30).WithMessage("{PropertyName} can not be more than 30 characters");

        }
    }
}
