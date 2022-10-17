using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    private const int MinLength = 3;
    private const int MinSeconds = 0;
    
    public CreateNoteCommandValidator()
    {
        RuleFor(note => note.Name)
            .NotEmpty().WithError(ValidationErrors.Note.NameIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.Note.NameMinLength);

        RuleFor(note => note.Content)
            .NotEmpty().WithError(ValidationErrors.Note.ContentIsRequired);

        RuleFor(note => note.Time)
            .NotNull().WithError(ValidationErrors.Note.TimeIsRequired)
            .GreaterThanOrEqualTo(MinSeconds).WithError(ValidationErrors.Note.TimeMinLength);

        RuleFor(note => note.VideoId)
            .NotNull().WithError(ValidationErrors.Note.VideoIdIsRequired);
    }
}