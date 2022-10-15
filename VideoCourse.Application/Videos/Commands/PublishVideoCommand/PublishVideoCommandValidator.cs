using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.PublishVideoCommand;

public class PublishVideoCommandValidator : AbstractValidator<PublishVideoCommand>
{
    public PublishVideoCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithError(ValidationErrors.Video.IdIsRequired);
    }
}