using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Api.Core.Common;
using VideoCourse.Application.Users.Commands;
using VideoCourse.Application.Users.Commands.AssignUserRolesCommand;
using VideoCourse.Application.Users.Commands.DeleteUserCommand;
using VideoCourse.Application.Users.Queries.GetAllRoles;
using VideoCourse.Application.Users.Queries.GetCreatorsQuery;
using VideoCourse.Application.Users.Queries.GetUsersQuery;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Api.Users;

public class UsersModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var usersGroup = app.MapGroup("api/users")
            .RequireAuthorization();

        usersGroup.MapPost("", CreateUser).AllowAnonymous();
        usersGroup.MapPost("GetUsersWithParams", GetAllUsersWithParams).RequireAuthorization(Permissions.ReadUser.ToString());
        usersGroup.MapDelete("{id:Guid}", DeleteUser);
        usersGroup.MapGet("creators", GetCreators);
        usersGroup.MapGet("roles", GetRoles);
        usersGroup.MapPost("roles", AssignUserRoles);
    }

    private static async Task<IResult> AssignUserRoles(
        [FromBody] AssignUserRolesCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
        
    }

    private static async Task<IResult> GetRoles(
        [FromServices] ISender sender)
    {
        var request = new GetRolesQuery();
        var response = await sender.Send(request);

        return TypedResults.Ok(response);
    }

    private static async Task<IResult> GetCreators(
        [FromServices] ISender sender)
    {
        var request = new GetCreatorsQuery();
        var response = await sender.Send(request);

        return TypedResults.Ok(response);
    }
    private static async Task<IResult> DeleteUser(
        [FromQuery]Guid id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var request = new DeleteUserCommand(id);
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.NoContent(),
            error => TypedResults.Problem(error.ToProblemDetails()));
        
    }

    private static async Task<IResult> GetAllUsersWithParams(
        [FromBody]GetUsersQuery request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }

    private static async Task<IResult> CreateUser(
        [FromBody]CreateUserCommand request,
        [FromServices]ISender sender)
    {
        var response = await sender.Send(request);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
}