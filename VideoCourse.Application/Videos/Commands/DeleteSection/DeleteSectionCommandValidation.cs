using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.DeleteSection;

public class DeleteSectionCommandValidation : AbstractValidator<DeleteSectionCommand>
{
    public DeleteSectionCommandValidation()
    {
        RuleFor(s => s.Id).NotEmpty().WithError(ValidationErrors.Section.IdIsRequired);
    }
}