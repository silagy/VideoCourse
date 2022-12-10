using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Users.Commands;
using VideoCourse.Application.Users.Commands.DeleteUserCommand;
using VideoCourse.Application.Users.Queries.GetAllRoles;
using VideoCourse.Application.Users.Queries.GetCreatorsQuery;
using VideoCourse.Application.Users.Queries.GetUsersQuery;
using VideoCourse.Domain.Enums;
using VideoCourse.Infrastructure.Common.Authentication;

namespace VideoCourse.Api.Controllers;


public class UsersController : ApiController
{
    private ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [HasPermission(Permissions.EditUser)]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateUserCommand request)
    {
        var result = await _sender.Send(request);

        return result.Match(
            results => Ok(results),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(Permissions.ReadUser)]
    [Route("GetUsersWithParams")]
    public async Task<IActionResult> GetAll(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);

        return result.Match(
            users => Ok(users),
            errors => Problem(errors));
    }

    [HttpDelete]
    [HasPermission(Permissions.DeleteUser)]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserCommand(id);
        var result = await _sender.Send(request, cancellationToken);

        return result.Match(
            user => NoContent(),
            errors => Problem(errors));
    }

    [HttpGet]
    [HasPermission(Permissions.ReadUser)]
    [Route("creators")]
    public async Task<IActionResult> GetCreators()
    {
        var request = new GetCreatorsQuery();
        var response = await _sender.Send(request);

        return Ok(response);
    }

    [HttpGet]
    [HasPermission(Permissions.ReadUser)]
    [Route("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var request = new GetRolesQuery();
        var response = await _sender.Send(request);

        return Ok(response);
    }
}