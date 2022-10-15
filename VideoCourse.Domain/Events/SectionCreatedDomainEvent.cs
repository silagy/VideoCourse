using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Events;

public record SectionCreatedDomainEvent(Guid Id):IDomainEvent;