using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;
using VideoCourse.Domain.DomainErrors;

namespace VideoCourse.Application.Users.Commands.AssignUserRolesCommand;

public class AssignUserRolesCommandValidator : AbstractValidator<AssignUserRolesCommand>
{
    public AssignUserRolesCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty().WithError(ValidationErrors.User.IdIsRequired);
        RuleFor(r => r.Roles)
            .NotEmpty().WithError(ValidationErrors.User.RoleIsRequired)
            .ForEach(r
                => r.IsInEnum().WithError(ValidationErrors.User.RoleMustBeValidEnum));
    }
}