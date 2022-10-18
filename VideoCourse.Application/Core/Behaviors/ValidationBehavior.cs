using ErrorOr;
using FluentValidation;
using MediatR;

namespace VideoCourse.Application.Core.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest: IRequest<TResponse>
where TResponse: IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(e => e is not null)
            .Select(f => Error.Validation(
                code: f.PropertyName,
                description: f.ErrorMessage))
            .ToList();

        if (!failures.Any())
        {
            return await next();
        }


        return (dynamic)failures;
    }
}