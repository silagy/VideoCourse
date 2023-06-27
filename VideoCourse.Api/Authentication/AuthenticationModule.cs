using Carter;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Api.Core.Common;
using VideoCourse.Application.Authentication.Queries;
using VideoCourse.Application.Users.Common;
using ISender = MediatR.ISender;

namespace VideoCourse.Api.Authentication;

public class AuthenticationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/Authentication/").AllowAnonymous();
        group.MapPost("Login", LoginHandler);
    }

    private static async Task<IResult> LoginHandler([FromBody] LoginQuery request,
        [FromServices] ISender sender)
    {
        var response = await sender.Send(request);

        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
}