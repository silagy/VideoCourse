using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Videos.Commands.CreateVideo;

namespace VideoCourse.Api.Controllers;

public class VideosController : ApiController
{
    private readonly ISender _sender;

    public VideosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVideoCommand request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);

        return response.Match(
            video => Ok(video),
            errors => Problem(errors));
    }
}