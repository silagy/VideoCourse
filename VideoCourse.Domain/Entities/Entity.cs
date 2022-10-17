using VideoCourse.Domain.Primitives;

namespace VideoCourse.Domain.Entities;

public abstract class Entity: IAuditableEntity, ISoftDeleteEntity
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public Entity(Guid id)
    {
        Id = id;
        IsDeleted = false;
    }

    protected Entity()
    {
    }
    public void MarkDeleted()
    {
        IsDeleted = true;
    }

    public void UpdateUpdateDate(DateTime updateDate)
    {
        UpdateDate = updateDate;
    }

    
}