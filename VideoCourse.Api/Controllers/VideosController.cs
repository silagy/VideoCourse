using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Videos.Commands.CreateSection;
using VideoCourse.Application.Videos.Commands.CreateVideo;
using VideoCourse.Application.Videos.Commands.DeleteSection;
using VideoCourse.Application.Videos.Commands.DeleteVideo;
using VideoCourse.Application.Videos.Queries.GetVideosWithSection;
using VideoCourse.Application.Videos.Queries.GetVidoesByCreatorId;

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

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteVideoCommand(id);
        var result = await _sender.Send(request);

        return result.Match(
            video => NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete]
    [Route("section/{id:Guid}")]
    public async Task<IActionResult> DeleteSection(Guid id)
    {
        var request = new DeleteSectionCommand(id);
        var result = await _sender.Send(request);

        return result.Match(
            section => NoContent(),
            errors => Problem(errors));
    }

    [HttpGet]
    [Route("GetVideosByCreatorId/{id:Guid}")]
    public async Task<IActionResult> GetVideosByCreatorId(Guid id)
    {
        var request = new GetVideosByCreatorIdQuery(id);
        var response = await _sender.Send(request);

        return Ok(response);
    }
}