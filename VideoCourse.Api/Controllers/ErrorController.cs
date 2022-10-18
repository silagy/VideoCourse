using Microsoft.AspNetCore.Mvc;

namespace VideoCourse.Api.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandelErrors()
    {
        return Problem();
    }
}