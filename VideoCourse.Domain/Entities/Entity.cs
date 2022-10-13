namespace VideoCourse.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    public Entity(Guid id, DateTime creationDate, DateTime updateDate)
    {
        Id = id;
        CreationDate = creationDate;
        UpdateDate = updateDate;
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