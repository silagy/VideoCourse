using MediatR;
using VideoCourse.Application.Core.Abstractions.Emails;
using VideoCourse.Application.Core.Abstractions.Emails.EmailTemplates;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.Events;

namespace VideoCourse.Application.Users.Events;

public class CreateUserDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public CreateUserDomainEventHandler(IEmailService emailService, IUserRepository userRepository)
    {
        _emailService = emailService;
        _userRepository = userRepository;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // Get the user from DB
        var userResponse = await _userRepository.GetByIdAsync(notification.UserId);

        if (userResponse.IsError)
        {
            return;
        }

        var user = userResponse.Value;

        var emailSentResult = await _emailService.SendWelcomeMessage(new WelcomeEmailMessage(
            user.Email.Value,
            user.FirstName,
            user.LastName
        ));

        return;
    }
}