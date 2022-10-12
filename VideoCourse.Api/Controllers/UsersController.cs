using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Users.Commands;

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
}