using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private const int MinLength = 3;
    private const int PasswordMinLength = 6;
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty().WithError(ValidationErrors.User.FirstNameIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.User.FirstNameMinLength);
        
        RuleFor(u => u.LastName)
            .NotEmpty().WithError(ValidationErrors.User.LastNameIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.User.LastNameMinLength);

        RuleFor(u => u.Email)
            .NotEmpty().WithError(ValidationErrors.User.EmailIsRequired)
            .EmailAddress().WithError(ValidationErrors.User.EmailIsValidEmailAddress);

        RuleFor(u => u.Password)
            .NotEmpty().WithError(ValidationErrors.User.PasswordIsRequired)
            .MinimumLength(PasswordMinLength).WithError(ValidationErrors.User.PasswordMinLength);

        RuleFor(u => u.Role)
            .NotEmpty().WithError(ValidationErrors.User.RoleIsRequired)
            .IsInEnum().WithError(ValidationErrors.User.RoleMustBeValidEnum);
    }
}