using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.CreateQuestion;

public class CreateQuestionOptionValidator : AbstractValidator<CreateQuestionOption>
{
    private const int MinLength = 3;
    public CreateQuestionOptionValidator()
    {
        RuleFor(qo => qo.Name)
            .NotEmpty().WithError(ValidationErrors.Question.QuestionOptionIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.Question.QuestionOptionMinLength);

        RuleFor(qo => qo.IsRight)
            .NotNull().WithError(ValidationErrors.Question.QuestionOptionIsRightRequired);
    }
}