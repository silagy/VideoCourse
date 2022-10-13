using System.ComponentModel.DataAnnotations;
using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Commands.CreateVideo;

public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
{
    private const int MinLength = 3;
    private const int MinSeconds = 0;
    private const string UrlRegex =
        @"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";
    
    public CreateVideoCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithError(ValidationErrors.Video.NameIsRequired)
            .MinimumLength(MinLength).WithError(ValidationErrors.Video.NameMinLength);
        
        RuleFor(v => v.Url)
            .NotEmpty().WithError(ValidationErrors.Video.UrlIsRequired)
            .Matches(UrlRegex).WithError(ValidationErrors.Video.UrlMustBeValidUrl);
        
        RuleFor(v => v.CreatorId)
            .NotEmpty().WithError(ValidationErrors.Video.CreatorIdIsRequired);

        RuleFor(v => v.Duration)
            .NotNull().WithError(ValidationErrors.Video.DurationIsRequired)
            .GreaterThan(MinSeconds).WithError(ValidationErrors.Video.DurationMinLength);
    }
}