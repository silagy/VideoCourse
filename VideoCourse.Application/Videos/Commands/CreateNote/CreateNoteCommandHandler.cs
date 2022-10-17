using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.DomainErrors;

namespace VideoCourse.Application.Videos.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, ErrorOr<VideoResponse>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<VideoResponse>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        // Get the video with all items
        var video = await _videoRepository.GetVideoWithAllContentById(request.VideoId);
        if (video is null)
        {
            return CustomErrors.Entity.EntityNotFound;
        }
        // Create new Note
        var noteResponse = video.AddNote(
            Guid.NewGuid(),
            request.Name,
            request.Content,
            request.Time
        );

        if (noteResponse.IsError)
        {
            return noteResponse.Errors;
        }
        
        // Add note to database
        await _videoRepository.AddNote(noteResponse.Value);
        await _unitOfWork.Commit();

        return new VideoResponse(
            video.Id,
            video.Url.Value,
            video.Name,
            video.Description,
            video.Duration.Value,
            video.CreatorId,
            video.CreationDate,
            video.UpdateDate,
            Sections: video.Sections.Select(s => new SectionResponse(
                s.Id,
                s.Name,
                s.Description,
                s.StartTime.Value,
                s.EndTime.Value,
                s.CreationDate,
                s.UpdateDate)),
            Items: video.GetAllItems().Select(it => new ItemResponse(
                it.Id,
                it.Name,
                it.Content,
                it.Time.Value,
                it.VideoId,
                it.CreationDate,
                it.UpdateDate)));
    }
}