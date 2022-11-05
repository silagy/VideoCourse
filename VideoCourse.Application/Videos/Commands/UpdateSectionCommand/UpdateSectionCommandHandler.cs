using ErrorOr;
using Mapster;
using MediatR;
using MediatR.Pipeline;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Videos.Commands.UpdateSectionCommand;

public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, ErrorOr<VideoResponse>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSectionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<VideoResponse>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var startTimeValidator = Duration.Create(request.StartTime);

        if (startTimeValidator.IsError)
        {
            return startTimeValidator.Errors;
        }

        var startTime = startTimeValidator.Value;

        var endTimeValidator = Duration.Create(request.EndTime);
        if (endTimeValidator.IsError)
        {
            return endTimeValidator.Errors;
        }

        var endTime = endTimeValidator.Value;
        
        // Get the video
        var videoResult = await _videoRepository.GetByIdWithSections(request.VideoId);

        if (videoResult.IsError)
        {
            return videoResult.Errors;
        }
        
        var video = videoResult.Value;
        
       // Create Section
       var section = video.UpdateSection(
           request.SectionId,
           request.Name,
           request.Description,
           startTime,
           endTime);
       
       if(section.IsError) return section.Errors;

        await _videoRepository.UpdateSection(section.Value);
        await _unitOfWork.Commit();

        return video.Adapt<VideoResponse>();

    }
}