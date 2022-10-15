using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Events;

public record VideoCreatedDomainEvent(Guid Id) : IDomainEvent;