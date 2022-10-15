using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Events;

public record PublishedVideoDomainEvent(Guid Id) : IDomainEvent;