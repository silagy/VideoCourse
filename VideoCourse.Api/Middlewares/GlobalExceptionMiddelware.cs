using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VideoCourse.Api.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
    private ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception er)
        {
            _logger.LogError(er, er.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An error server has occurred"
            };

            string json = JsonConvert.SerializeObject(problem);
            await context.Response.WriteAsync(json);
            context.Response.ContentType = "application/json";
        }
    }
}