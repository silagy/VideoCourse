using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;

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
        var videoResponse = await _videoRepository.GetVideoWithAllContentById(request.VideoId);

        if (videoResponse.IsError) return videoResponse.Errors;

        var video = videoResponse.Value;
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

        return video.Adapt<VideoResponse>();
    }
}