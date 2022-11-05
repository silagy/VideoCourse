using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.UpdateVideoDescriptionCommand;

public class UpdateVideoDescriptionCommandValidator : AbstractValidator<UpdateVideoDescriptionCommand>
{
    private const int MinLength = 3;
    
    public UpdateVideoDescriptionCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithError(ValidationErrors.Video.NameIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.Video.NameMinLength);
        
        RuleFor(v => v.Id).NotEmpty().WithError(ValidationErrors.Video.IdIsRequired);
    }
}