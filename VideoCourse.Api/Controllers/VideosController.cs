using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Videos.Commands.CreateSection;
using VideoCourse.Application.Videos.Commands.CreateVideo;
using VideoCourse.Application.Videos.Queries.GetVideosWithSection;

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

    [HttpPost]
    [Route("section")]
    public async Task<IActionResult> CreateSection(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);

        return result.Match(
            section => Ok(section),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetVideo(Guid id)
    {
        var request = new GetVideoWithSectionsQuery(id);
        var result = await _sender.Send(request);

        return result.Match(
            video => Ok(video),
            errors => Problem(errors));
    }
}