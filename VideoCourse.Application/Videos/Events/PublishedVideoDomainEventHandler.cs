using MediatR;
using VideoCourse.Application.Core.Abstractions.Emails;
using VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.Events;

namespace VideoCourse.Application.Videos.Events;

public class PublishedVideoDomainEventHandler : INotificationHandler<PublishedVideoDomainEvent>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IEmailService _emailService;

    public PublishedVideoDomainEventHandler(IVideoRepository videoRepository, IEmailService emailService)
    {
        _videoRepository = videoRepository;
        _emailService = emailService;
    }

    public async Task Handle(PublishedVideoDomainEvent notification, CancellationToken cancellationToken)
    {
        // Get the video with user from database
        var videoResult = await _videoRepository.GetByIdWithCreator(notification.Id);

        if (videoResult.IsError)
        {
            return;
        }

        var video = videoResult.Value;
        var emailResult = await _emailService.SendPublishedVideoMessage(new VideoPublishedEmailMessage(
            emailTo: video.Creator.Email,
            videoId: video.Id,
            video.Url,
            video.Creator.FirstName,
            video.Creator.LastName,
            video.Name
        ));

        return;
    }
}