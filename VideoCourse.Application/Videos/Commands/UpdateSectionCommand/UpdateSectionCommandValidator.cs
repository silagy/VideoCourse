using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.UpdateSectionCommand;

public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    private const int MinLength = 3;
    private const int MinSeconds = -1;

    public UpdateSectionCommandValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty()
            .MinimumLength(MinLength);

        RuleFor(s => s.StartTime)
            .NotNull()
            .GreaterThan(MinSeconds);
        
        RuleFor(s => s.EndTime)
            .NotNull()
            .GreaterThan(MinSeconds);

        RuleFor(s => s.VideoId)
            .NotEmpty();

        RuleFor(s => s.SectionId)
            .NotEmpty().WithError(ValidationErrors.Section.IdIsRequired);
    }
}