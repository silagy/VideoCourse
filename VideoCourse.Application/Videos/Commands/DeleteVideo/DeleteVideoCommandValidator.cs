using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.DeleteVideo;

public class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
{
    public DeleteVideoCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty().WithError(ValidationErrors.Video.IdIsRequired);
    }
}