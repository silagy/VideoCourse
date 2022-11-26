using ErrorOr;

namespace VideoCourse.Domain.Shared;

public static class ErrorOrExtensions
{
    public static ErrorOr<T> Ensure<T>(this ErrorOr<T> result,
        Func<T, bool> predicate,
        Error error)
    {
        if (result.IsError)
        {
            return result;
        }

        return predicate(result.Value) ? result : error;
    }

    public static ErrorOr<TOut> Map<TIn, TOut>(this ErrorOr<TIn> result,
        Func<TIn, TOut> mappingFunc)
    {
        return result.IsError ? result.Errors : mappingFunc(result.Value);
    }
}