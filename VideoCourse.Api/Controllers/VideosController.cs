using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Application.Videos.Commands.CreateNote;
using VideoCourse.Application.Videos.Commands.CreateQuestion;
using VideoCourse.Application.Videos.Commands.CreateSection;
using VideoCourse.Application.Videos.Commands.CreateTextQuestion;
using VideoCourse.Application.Videos.Commands.CreateVideo;
using VideoCourse.Application.Videos.Commands.DeleteNoteCommand;
using VideoCourse.Application.Videos.Commands.DeleteQuestion;
using VideoCourse.Application.Videos.Commands.DeleteSection;
using VideoCourse.Application.Videos.Commands.DeleteVideo;
using VideoCourse.Application.Videos.Commands.PublishVideoCommand;
using VideoCourse.Application.Videos.Commands.UpdateSectionCommand;
using VideoCourse.Application.Videos.Commands.UpdateVideoDescriptionCommand;
using VideoCourse.Application.Videos.Queries.GetVideos;
using VideoCourse.Application.Videos.Queries.GetVideosWithSection;
using VideoCourse.Application.Videos.Queries.GetVidoesByCreatorId;
using VideoCourse.Domain.Enums;
using VideoCourse.Infrastructure.Common.Authentication;

namespace VideoCourse.Api.Controllers;

public class VideosController : ApiController
{
    private readonly ISender _sender;

    public VideosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [HasPermission(Permissions.EditVideos)]
    public async Task<IActionResult> Create(CreateVideoCommand request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);

        return response.Match(
            video => Ok(video),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(Permissions.EditVideos)]
    [Route("section")]
    public async Task<IActionResult> CreateSection(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);

        return result.Match(
            section => Ok(section),
            errors => Problem(errors));
    }

    [HttpGet]
    [HasPermission(Permissions.ReadVideos)]
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
    [HasPermission(Permissions.DeleteVideos)]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteVideoCommand(id);
        var result = await _sender.Send(request);

        return result.Match(
            video => NoContent(),
            errors => Problem(errors));
    }

    [HttpPut]
    [HasPermission(Permissions.EditVideos)]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateVideoDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var requestUpdate = new UpdateVideoDescriptionCommand(id, request.Name, request.Description);
        var result = await _sender.Send(requestUpdate, cancellationToken);

        return result.Match(
            video => Ok(video),
            errors => Problem(errors));
    }

    [HttpDelete]
    [HasPermission(Permissions.EditVideos)]
    [Route("section/{id:Guid}")]
    public async Task<IActionResult> DeleteSection(Guid id)
    {
        var request = new DeleteSectionCommand(id);
        var result = await _sender.Send(request);

        return result.Match(
            section => NoContent(),
            errors => Problem(errors));
    }

    [HttpPut]
    [HasPermission(Permissions.EditVideos)]
    [Route("section/{id:Guid}")]
    public async Task<IActionResult> UpdateSection(Guid id, UpdateSectionCommand request,
        CancellationToken cancellationToken)
    {
        var requestOperation = new UpdateSectionCommand(
            request.Name,
            request.Description,
            request.StartTime,
            request.EndTime,
            request.VideoId,
            id);
        var result = await _sender.Send(requestOperation, cancellationToken);

        return result.Match(
            video => Ok(video),
            errors => Problem(errors));
    }

    [HttpGet]
    [HasPermission(Permissions.ReadVideos)]
    [Route("GetVideosByCreatorId/{id:Guid}")]
    public async Task<IActionResult> GetVideosByCreatorId(Guid id)
    {
        var request = new GetVideosByCreatorIdQuery(id);
        var response = await _sender.Send(request);

        return Ok(response);
    }

    [HttpPost]
    [HasPermission(Permissions.EditVideos)]
    [Route("publish/{id:Guid}")]
    public async Task<IActionResult> PublishVideo(Guid id)
    {
        var request = new PublishVideoCommand(id);
        var response = await _sender.Send(request);

        return response.Match(
            video => Ok(video),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(Permissions.EditVideos)]
    [Route("notes")]
    public async Task<IActionResult> AddNote(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request,cancellationToken);

        return result.Match(
            note => Ok(note),
            errors => Problem(errors));

    }

    [HttpDelete]
    [HasPermission(Permissions.EditVideos)]
    [Route(("{videoId:Guid}/notes/{id:Guid}"))]
    public async Task<IActionResult> DeleteNote(Guid videoId, Guid id, CancellationToken cancellationToken)
    {
        DeleteNoteCommand request = new DeleteNoteCommand(id, videoId);
        var result = await _sender.Send(request, cancellationToken);

        return result.Match(
            note => NoContent(),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(Permissions.EditVideos)]
    [Route("questions/multiple-answers")]
    public async Task<IActionResult> AddQuestion(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request);

        return result.Match(
            question => Ok(question),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(Permissions.EditVideos)]
    [Route("questions/text-question")]
    public async Task<IActionResult> AddTextQuestion(CreateTextQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);

        return result.Match(
            video => Ok(video),
            errors => Problem(errors));
    }

    [HttpDelete]
    [HasPermission(Permissions.EditVideos)]
    [Route("{videoId:Guid}/questions/{id:Guid}")]
    public async Task<IActionResult> DeleteQuestion(Guid videoId, Guid id, CancellationToken cancellationToken)
    {
        DeleteQuestionCommand request = new DeleteQuestionCommand(id, videoId);
        var results = await _sender.Send(request, cancellationToken);

        return results.Match(
            question => NoContent(),
            errors => Problem(errors));
    }

    [HttpPost]
    [HasPermission(Permissions.ReadVideos)]
    [Route("videos-list")]
    public async Task<IActionResult> GetVideosList(GetVideosQuery request, CancellationToken cancellationToken)
    {
        var videosResponse = await _sender.Send(request, cancellationToken);
        return videosResponse.Match(
            videos => Ok(videos),
            errors => Problem(errors));
    }

}