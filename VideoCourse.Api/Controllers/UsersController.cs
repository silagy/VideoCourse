using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Users.Commands;
using VideoCourse.Application.Users.Commands.DeleteUserCommand;
using VideoCourse.Application.Users.Queries.GetCreatorsQuery;
using VideoCourse.Application.Users.Queries.GetUsersQuery;

namespace VideoCourse.Api.Controllers;


public class UsersController : ApiController
{
    private ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateUserCommand request)
    {
        var result = await _sender.Send(request);

        return result.Match(
            results => Ok(results),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("GetUsersWithParams")]
    public async Task<IActionResult> GetAll(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);

        return result.Match(
            users => Ok(users),
            errors => Problem(errors));
    }

    [HttpDelete]
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
    [Route("creators")]
    public async Task<IActionResult> GetCreators()
    {
        var request = new GetCreatorsQuery();
        var response = await _sender.Send(request);

        return Ok(response);
    }
}