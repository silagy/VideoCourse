using FluentValidation;
using VideoCourse.Application.Core.Extensions;
using VideoCourse.Application.Core.ValidationErrors;

namespace VideoCourse.Application.Videos.Queries.GetVideos;

public class GetVideosQueryValidator : AbstractValidator<GetVideosQuery>
{
    private const int MinPage = 1;
    private const int MinSearchTerm = 3;
        
    public GetVideosQueryValidator()
    {
        RuleFor(v => v.Page)
            .NotNull().WithError(ValidationErrors.PageResult.PageIsRequired)
            .GreaterThanOrEqualTo(MinPage).WithError(ValidationErrors.PageResult.MustBeGreaterThanZero);

        RuleFor(v => v.PageSize)
            .NotNull().WithError(ValidationErrors.PageResult.PageSizeIsRequired)
            .GreaterThanOrEqualTo(MinPage).WithError(ValidationErrors.PageResult.MustBeGreaterThanZero);

        RuleFor(v => v.SearchTerm)
            .MinimumLength(MinSearchTerm).WithError(ValidationErrors.Search.SearchTermMustBeGreaterThanEqualToThree);
    }
}