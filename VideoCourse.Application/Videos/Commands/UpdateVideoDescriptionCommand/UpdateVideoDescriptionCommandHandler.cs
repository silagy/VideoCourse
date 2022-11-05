using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Commands.UpdateVideoDescriptionCommand;

public class UpdateVideoDescriptionCommandHandler : IRequestHandler<UpdateVideoDescriptionCommand, ErrorOr<BasicVideoResponse>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVideoDescriptionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<BasicVideoResponse>> Handle(UpdateVideoDescriptionCommand request, CancellationToken cancellationToken)
    {
        // Get the video
        var videoResponse = await _videoRepository.GetById(request.Id);

        if (videoResponse.IsError) return videoResponse.Errors;

        var video = videoResponse.Value;
        
        video.UpdateVideoDetails(request.Name, request.Description);

        var videoUpdateResponse = _videoRepository.Update(video);

        if (videoUpdateResponse.IsError) return videoUpdateResponse.Errors;

        await _unitOfWork.Commit();

        return video.Adapt<BasicVideoResponse>();
    }
}