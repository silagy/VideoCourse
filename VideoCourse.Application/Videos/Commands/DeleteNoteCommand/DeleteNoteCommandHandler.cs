using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Videos.Commands.DeleteNoteCommand;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, ErrorOr<bool>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNoteCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        // Getting the video
        ErrorOr<Video> videoRequest = await _videoRepository.GetVideoWithAllContentById(request.VideoId);

        if (videoRequest.IsError) return videoRequest.Errors;
        
        // Get the note
        Video video = videoRequest.Value;

        Note? note = video.Notes.FirstOrDefault(n => n.Id == request.Id);

        if (note is null) return CustomErrors.Entity.EntityNotFound;

        ErrorOr<bool> deleteResult = await _videoRepository.RemoveNote(note);

        if (deleteResult.IsError) return CustomErrors.Entity.EntityCannotBeDeleted;

        await _unitOfWork.Commit();

        return true;
    }
}