using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Domain.DomainErrors;

namespace VideoCourse.Application.Videos.Commands.DeleteNoteCommand;

public class DeleteNoteCommandValidation : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidation()
    {
        RuleFor(n => n.Id)
            .NotEmpty().WithError(CustomErrors.Entity.EmptyGuid);

        RuleFor(n => n.VideoId)
            .NotEmpty().WithError(CustomErrors.Entity.EmptyGuid);
    }
}