namespace VideoCourse.Domain.Primitives;

public interface ISoftDeleteEntity
{
    public bool IsDeleted { get; set; }
}