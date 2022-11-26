namespace VideoCourse.Domain.Primitives;

public interface IAuditableEntity
{
   public DateTime CreationDate { get;  set; }
   public DateTime UpdateDate { get; set; }
}