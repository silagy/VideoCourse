using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Authentication.Queries;

namespace VideoCourse.Api.Controllers;

[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginQuery request)
    {
        var response = await _sender.Send(request);

        return response.Match(
            user => Ok(user),
            errors => Problem(errors));
    }
}