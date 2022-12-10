using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Domain.DomainErrors;

namespace VideoCourse.Application.Videos.Commands.DeleteQuestion;

public class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
{
    public DeleteQuestionCommandValidator()
    {
        RuleFor(n => n.Id)
            .NotEmpty().WithError(CustomErrors.Entity.EmptyGuid);

        RuleFor(n => n.VideoId)
            .NotEmpty().WithError(CustomErrors.Entity.EmptyGuid);
    }
}