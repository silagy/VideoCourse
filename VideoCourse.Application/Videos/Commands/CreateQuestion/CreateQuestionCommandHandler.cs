using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.CreateQuestion;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ErrorOr<VideoResponse>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuestionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<VideoResponse>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        // Get the video
        var videoResponse = await _videoRepository.GetVideoWithAllContentById(request.VideoId);

        if (videoResponse.IsError) return videoResponse.Errors;

        var video = videoResponse.Value;
        
        // Create the question
        var questionResponse = video.AddQuestion(Guid.NewGuid(),
            request.Name,
            request.Feedback,
            request.Content,
            request.Time,
            request.VideoId);

        if (questionResponse.IsError) return questionResponse.Errors;

        await _videoRepository.AddQuestion(questionResponse.Value);
        await _unitOfWork.Commit();

        return video.Adapt<VideoResponse>();

    }
}