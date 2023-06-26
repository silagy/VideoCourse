using ErrorOr;

namespace VideoCourse.Api.Middlewares;

public class CustomProblemDetailsWriter : IProblemDetailsWriter
{
    public ValueTask WriteAsync(ProblemDetailsContext context)
    {
        var response = context.HttpContext.Response;
        var error = context.HttpContext.Features.Get<Error>();

        return new ValueTask(response.WriteAsJsonAsync(context.ProblemDetails));
    }

    public bool CanWrite(ProblemDetailsContext context)
    {
        return context.HttpContext.Response.StatusCode ==  400;
    }
}