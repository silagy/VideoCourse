using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Videos.Commands.CreateTextQuestion;

public class CreateTextQuestionCommandHandler : IRequestHandler<CreateTextQuestionCommand, ErrorOr<VideoResponse>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTextQuestionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<VideoResponse>> Handle(CreateTextQuestionCommand request, CancellationToken cancellationToken)
    {
        // Get the video
        var videoResponse = await _videoRepository.GetVideoWithAllContentById(request.VideoId);

        if (videoResponse.IsError) return videoResponse.Errors;

        var video = videoResponse.Value;

        var questionResponse = video.AddQuestion(
            Guid.NewGuid(),
            request.Name,
            request.Feedback,
            request.Content,
            request.Time,
            request.VideoId,
            QuestionType.TextCompletion);

        if (questionResponse.IsError) return questionResponse.Errors;
        
        // Add question to database
        await _videoRepository.AddQuestion(questionResponse.Value);
        await _unitOfWork.Commit();

        return video.Adapt<VideoResponse>();
    }
}