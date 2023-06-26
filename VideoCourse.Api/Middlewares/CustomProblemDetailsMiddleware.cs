using ErrorOr;

namespace VideoCourse.Api.Middlewares;

public class CustomProblemDetailsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IProblemDetailsWriter _detailsWriter;

    public CustomProblemDetailsMiddleware(RequestDelegate next, IProblemDetailsWriter detailsWriter)
    {
        _next = next;
        _detailsWriter = detailsWriter;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        var error = context.Features.Get<Error>();
        if (error is { })
        {
            if(_detailsWriter.CanWrite(new ProblemDetailsContext()
               {
                   HttpContext = context
               }))
            {
                await _detailsWriter.WriteAsync(new ProblemDetailsContext()
                {
                    HttpContext = context,
                    ProblemDetails =
                    {
                        Title = error.Code,
                        Detail = error.Description,
                    }
                });
            }
        }
    }
}

public static class AddCustomProblemDetails
{
    public static IApplicationBuilder CustomProblemDetails(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomProblemDetailsMiddleware>();
    }
}