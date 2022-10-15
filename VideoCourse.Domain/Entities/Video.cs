using ErrorOr;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Primitives;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class Video : AggregateRoot
{
    public VideoUrl Url { get; private set; }
    public string Name { get; private set; }
    public bool IsDeleted { get; set; }
    public string? Description { get; private set; }
    public Duration Duration { get; set; }
    public Guid CreatorId { get; private set; }
    public User Creator { get; set; } = null!;
    
    // List of sections
    private List<Section> _sections { get; set; } = new();
    public IReadOnlyCollection<Section> Sections => _sections;

    protected Video(): base()
    {
    }
    
    public Video(
        Guid id,
        DateTime creationDate,
        DateTime updateDate,
        VideoUrl url,
        string name,
        string? description,
        Duration duration,
        Guid creatorId) 
        : base(id, creationDate, updateDate)
    {
        Url = url;
        Name = name;
        Description = description;
        Duration = duration;
        CreatorId = creatorId;
    }

    public ErrorOr<Section> AddSection(
        Guid id,
        string name,
        string? description,
        Duration startTime,
        Duration endTime,
        DateTime creationDate,
        DateTime updateDate)
    {
        // Check if section with the same name already exits
        if (this.Sections.Any(s => s.Name == name))
        {
            return CustomErrors.Video.SectionNameAlreadyExists;
        }

        foreach (var item in Sections)
        {
            if (startTime.Value < item.EndTime)
            {
                return CustomErrors.Video.SectionStartTimeMustBeSequential;
            }
        }
        
        var section = Section.Create(
            id,
            name,
            description,
            startTime,
            endTime,
            this.Id,
            creationDate,
            updateDate);
        if (section.IsError)
        {
            return section;
        }
        
        _sections.Add(section.Value);
        return section;
    }


}