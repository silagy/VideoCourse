using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Videos.Commands.CreateSection;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, ErrorOr<VideoResponse>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTimeProvider;

    public CreateSectionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork, IDateTime dateTimeProvider, ISectionRepository sectionRepository)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _sectionRepository = sectionRepository;
    }

    public async Task<ErrorOr<VideoResponse>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        // Validate section attributes
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
        var section = video.AddSection(
            Guid.NewGuid(),
            name:request.Name,
            request.Description,
            startTime,
            endTime,
            _dateTimeProvider.UtcNow,
            _dateTimeProvider.UtcNow);

        if (section.IsError)
        {
            return section.Errors;
        }
        
        // Save video
        var sectionResult = await _videoRepository.AddSection(section.Value);
         if (sectionResult.IsError)
         {
             return sectionResult.Errors;
         }

         
        video.UpdateUpdateDate(_dateTimeProvider.UtcNow);
        await _unitOfWork.Commit();

        // Need to transform to video response
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
                s.UpdateDate))
        );
    }
}