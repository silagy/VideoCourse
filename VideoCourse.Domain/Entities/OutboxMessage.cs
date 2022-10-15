namespace VideoCourse.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public DateTime OccurredOnUtc { get; set; }
    public DateTime? PublishedOnUtc { get; set; }
    public string? Error { get; set; }
}