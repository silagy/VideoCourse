using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Events;

public record UserCreatedDomainEvent(Guid UserId): IDomainEvent;