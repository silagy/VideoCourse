namespace VideoCourse.Domain.Entities;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id, DateTime creationDate, DateTime updateDate) 
        : base(id, creationDate, updateDate)
    {
    }
    
    protected AggregateRoot(){}
}