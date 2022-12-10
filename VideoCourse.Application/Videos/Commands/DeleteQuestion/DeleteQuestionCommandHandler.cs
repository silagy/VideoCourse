using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Videos.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ErrorOr<bool>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteQuestionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        // Get the vidoe
        ErrorOr<Video> videoRequest = await _videoRepository.GetById(request.VideoId);

        if (videoRequest.IsError) return videoRequest.Errors;

        Video video = videoRequest.Value;

        Question? question = video.Questions.FirstOrDefault(q => q.Id == request.Id);

        if (question is null) return CustomErrors.Entity.EntityNotFound;

        var deleteRequest = await _videoRepository.RemoveQuestion(question);

        return !deleteRequest.IsError;
    }
}