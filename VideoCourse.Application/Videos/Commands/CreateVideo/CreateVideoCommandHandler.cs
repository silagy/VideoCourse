using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Videos.Commands.CreateVideo;

public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, ErrorOr<BasicVideoResponse>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTimeProvider;
    private readonly IUserRepository _userRepository;

    public CreateVideoCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork,
        IDateTime dateTimeProvider, IUserRepository userRepository)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<BasicVideoResponse>> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
    {
        // Validate Attributes
        var durationValidator = Duration.Create(request.Duration);

        if (durationValidator.IsError)
        {
            return durationValidator.Errors;
        }

        var duration = durationValidator.Value;
        
        var urlValidator = VideoUrl.Create(request.Url);

        if (urlValidator.IsError)
        {
            return urlValidator.Errors;
        }

        var url = urlValidator.Value;
        
        
        // Check if creator exists
        var creator = await _userRepository.GetByIdAsync(request.CreatorId);
        if (creator.IsError)
        {
            return creator.Errors;
        }

        if (creator.Value is null)
        {
            return CustomErrors.Video.CreatorNotFound;
        }
        
        // Creating video entity
        var video = new Video(
            Guid.NewGuid(),
            _dateTimeProvider.UtcNow,
            _dateTimeProvider.UtcNow,
            url,
            request.Name,
            request.Description,
            duration,
            creator.Value.Id);
        
        // Adding video
        await _videoRepository.Create(video);
        
        // Save changes
        await _unitOfWork.Commit();

        return video.Adapt<BasicVideoResponse>();
    }
}