using VideoCourse.Domain.Abstractions;

namespace VideoCourse.Domain.Entities;

public abstract class AggregateRoot : Entity
{
    private List<IDomainEvent> _domainEvents { get; set; } = new();

    public List<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
    protected AggregateRoot(Guid id) 
        : base(id)
    {
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    
    public void RaiseDomainEvent(IDomainEvent domainEvent) {
        _domainEvents.Add(domainEvent);
    }
    
    protected AggregateRoot(){}


    
}