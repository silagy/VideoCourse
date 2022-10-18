using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.CreateQuestion;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionCommand>
{
    private const int MinLength = 3;
    private const int MinSeconds = 0;
    public CreateQuestionValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty().WithError(ValidationErrors.Question.NameIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.Question.NameMinLength);

        RuleFor(q => q.Content)
            .NotEmpty().WithError(ValidationErrors.Question.ContentIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.Question.ContentMinLength);

        RuleFor(q => q.Time)
            .NotNull().WithError(ValidationErrors.Question.TimeIsRequired)
            .GreaterThanOrEqualTo(MinSeconds).WithError(ValidationErrors.Question.TimeMinLength);

        RuleFor(q => q.VideoId)
            .NotEmpty().WithError(ValidationErrors.Question.VideoIdIsRequired);
    }
}