using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;

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
            request.VideoId,
            QuestionType.MultipleAnswersSingleSelection);

        if (questionResponse.IsError) return questionResponse.Errors;
        var question = questionResponse.Value;

        foreach (var option in request.Options)
        {
            ErrorOr<QuestionOption> questionOptionResponse = question.AddOption(
                Guid.NewGuid(),
                option.Name,
                option.IsRight
            );

            if (questionOptionResponse.IsError) return questionOptionResponse.Errors;
        }

        if (question.QuestionTypeId == (int)QuestionType.MultipleAnswersSingleSelection
            && !question.QuestionOptions.Any(qo => qo.IsRight))
            return CustomErrors.Question.MultipleAnswersSingleSelectionMustHaveOneRightAnswer;

        await _videoRepository.AddQuestion(questionResponse.Value);
        await _unitOfWork.Commit();

        return video.Adapt<VideoResponse>();

    }
}