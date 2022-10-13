using ErrorOr;
using FluentValidation;

namespace VideoCourse.Application.Core.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error? error)
    {
        if (!error.HasValue)
        {
            throw new AggregateException("error is required");
        }

        return rule.WithErrorCode(error.Value.Code).WithMessage(error.Value.Description);
    }
}