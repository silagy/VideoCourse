using Carter;
using Microsoft.AspNetCore.Mvc;
using VideoCourse.Api.Core.Common;
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
using ISender = MediatR.ISender;

namespace VideoCourse.Api.Vidoes;

public class VideosModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var videoRoutes = app.MapGroup("api/videos/")
            .RequireAuthorization();
        
        // Videos Routes
        videoRoutes.MapGet("{id:Guid}", GetVideoHandler)
            .RequireAuthorization(Permissions.ReadVideos.ToString());
        videoRoutes.MapGet("GetVideosByCreatorId/{id:Guid}", GetVideosByCreatorIdHandler)
            .RequireAuthorization(Permissions.ReadVideos.ToString());
        videoRoutes.MapPost("", CreateVideo)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapDelete("{id:Guid}", DeleteVideoHandler)
            .RequireAuthorization(Permissions.DeleteVideos.ToString());
        videoRoutes.MapPut("{id:Guid}", EditVideoHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapPost("publish/{id:Guid}", PublishVideoHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapPost("videos-list", GetVideoListQueryHandler)
            .RequireAuthorization(Permissions.ReadVideos.ToString());
        
        // Sections Routes
        videoRoutes.MapPost("section", CreateSectionHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapDelete("section/{id:Guid}", DeleteSectionHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapPut("section/{id:Guid}", UpdateSectionHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        
        
        // Notes routes
        videoRoutes.MapPost("notes", AddNoteHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapDelete("{videoId:Guid}/notes/{id:Guid}", DeleteNoteHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        
        // Questions routes
        videoRoutes.MapPost("questions/multiple-answers", AddMultipleAnswersQuestionHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapPost("questions/text-question", AddTextQuestionHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        videoRoutes.MapDelete("{videoId:Guid}/questions/{id:Guid}", DeleteQuestionHandler)
            .RequireAuthorization(Permissions.EditVideos.ToString());
        
    }

    private static async Task<IResult> GetVideoListQueryHandler(
        [FromBody] GetVideosQuery request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }

    private static async Task<IResult> DeleteQuestionHandler(
        [FromRoute] Guid videoId,
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var request = new DeleteQuestionCommand(id, videoId);
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
    private static async Task<IResult> AddTextQuestionHandler(
        [FromBody] CreateTextQuestionCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
    private static async Task<IResult> AddMultipleAnswersQuestionHandler(
        [FromBody] CreateQuestionCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }

    private static async Task<IResult> DeleteNoteHandler(
        [FromRoute] Guid videoId,
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var request = new DeleteNoteCommand(id, videoId);
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }

    private static async Task<IResult> AddNoteHandler(
        [FromBody] CreateNoteCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }

    private static async Task<IResult> PublishVideoHandler(
        [FromRoute] Guid id,
        [FromServices] ISender sender)
    {
        var request = new PublishVideoCommand(id);
        var response = await sender.Send(request);
        
        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
    private static async Task<IResult> GetVideosByCreatorIdHandler(
        [FromRoute] Guid id,
        [FromServices] ISender sender)
    {
        var request = new GetVideosByCreatorIdQuery(id);
        var response = await sender.Send(request);

        return TypedResults.Ok(response);
    }

    private static async Task<IResult> GetVideoHandler(
        [FromRoute]Guid id,
        [FromServices]ISender sender)
    {
        var request = new GetVideoWithSectionsQuery(id);
        var response = await sender.Send(request);

        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
    private static async Task<IResult> UpdateSectionHandler(
        [FromRoute] Guid id,
        [FromBody] UpdateSectionCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var updateSectionCommand = new UpdateSectionCommand(
            request.Name,
            request.Description,
            request.StartTime,
            request.EndTime,
            request.VideoId,
            id);
        var response = await sender.Send(updateSectionCommand);

        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
    private static async Task<IResult> DeleteSectionHandler(
        [FromRoute] Guid id,
        [FromServices] ISender sender)
    {
        var request = new DeleteSectionCommand(id);
        var response = await sender.Send(request);

        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
    private static async Task<IResult> EditVideoHandler(
        [FromRoute] Guid id,
        [FromBody] UpdateVideoDescriptionCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var requestUpdateCommand = new UpdateVideoDescriptionCommand(id, request.Name, request.Description);
        var response = await sender.Send(requestUpdateCommand);

        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }

    private static async Task<IResult> DeleteVideoHandler(
        [FromRoute] Guid id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var request = new DeleteVideoCommand(id);
        var response = await sender.Send(request);

        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }

    private static async Task<IResult> CreateSectionHandler(
        [FromBody] CreateSectionCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);

        return response.Match<IResult>(
            section => TypedResults.Ok(section),
            errors => TypedResults.Problem(errors.ToProblemDetails()));
    }

    private static async Task<IResult> CreateVideo(
        [FromBody] CreateVideoCommand request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);

        return response.Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.Problem(error.ToProblemDetails()));
    }
}