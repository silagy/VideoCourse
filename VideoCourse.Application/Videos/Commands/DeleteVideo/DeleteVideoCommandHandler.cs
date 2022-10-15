using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;

namespace VideoCourse.Application.Videos.Commands.DeleteVideo;

public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, ErrorOr<bool>>
{
    private readonly IVideoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVideoCommandHandler(IVideoRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
    {
        // Get the video
        var videoResult = await _repository.GetById(request.Id);

        if (videoResult.IsError)
        {
            return videoResult.Errors;
        }

        var operationResult = await _repository.Remove(videoResult.Value);
        await _unitOfWork.Commit();

        return operationResult;
    }
}