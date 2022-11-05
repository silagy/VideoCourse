using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Events;

public record UserDeletedDomainEvent(Guid Id):IDomainEvent;