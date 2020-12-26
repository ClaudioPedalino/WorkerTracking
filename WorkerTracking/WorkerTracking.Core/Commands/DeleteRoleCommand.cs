using FluentValidation;
using MediatR;
using WorkerTracking.Core.Commands.Base;

namespace WorkerTracking.Core.Commands
{
    public class DeleteRoleCommand : IRequest<BaseCommandResponse>
    {
        public int RoleId { get; set; }
    }

    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} can not be null")
                .NotEmpty().WithMessage("{PropertyName} can not be empty");
        }
    }
}
