using FluentValidation;
using MediatR;
using WorkerTracking.Core.Commands.Base;
using WorkerTracking.Core.Identity;

namespace WorkerTracking.Core.Commands
{
    public class DeleteRoleCommand : LoggedRequest, IRequest<BaseCommandResponse>
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
