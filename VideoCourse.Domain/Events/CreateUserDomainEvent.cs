using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Events;

public record CreateUserDomainEvent(Guid UserId): IDomainEvent;