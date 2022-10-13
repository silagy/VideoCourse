using FluentValidation;

namespace VideoCourse.Application.Videos.Commands.CreateSection;

public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
{
    private const int MinLength = 3;
    private const int MinSeconds = -1;

    public CreateSectionCommandValidator()
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
    }
}