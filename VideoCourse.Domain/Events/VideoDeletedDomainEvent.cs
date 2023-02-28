using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Events;

public record VideoDeletedDomainEvent(Guid Id) : IDomainEvent;