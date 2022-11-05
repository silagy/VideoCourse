using ErrorOr;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class Section : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Duration StartTime { get; private set; }
    public Duration EndTime { get; private set; }
    public Guid VideoId { get; private set; }
    public Video Video { get; set; } = null!;

    protected Section()
    {
    }
    private Section(
        Guid id,
        string name,
        string? description,
        Duration startTime,
        Duration endTime,
        Guid videoId) 
        : base(id)
    {
        Name = name;
        StartTime = startTime;
        EndTime = endTime;
        VideoId = videoId;
        Description = description;
    }

    internal static ErrorOr<Section> Create(
        Guid id,
        string name,
        string? description,
        Duration startTime,
        Duration endTime,
        Guid videoId,
        DateTime creationDate,
        DateTime updateDate)
    {
        if (startTime.Equals(endTime) || startTime.Value > endTime.Value)
        {
            return Error.Validation(
                code: "StartTime.IsEqualOrGreaterThanEndTime",
                description: $"The start time must be less than end time {endTime}");
        }
        return new Section(id, name, description, startTime, endTime, videoId);
    }

    public ErrorOr<Section> UpdateSection(
        string name,
        string? description,
        Duration startTime,
        Duration endTime)
    {
        if (startTime.Equals(endTime) || startTime.Value > endTime.Value)
        {
            return Error.Validation(
                code: "StartTime.IsEqualOrGreaterThanEndTime",
                description: $"The start time must be less than end time {endTime}");
        }

        Name = name;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;

        return this;
    }
}