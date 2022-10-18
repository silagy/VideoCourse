using ErrorOr;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Events;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class Video : AggregateRoot
{
    public VideoUrl Url { get; private set; }
    public string Name { get; private set; }
    public bool IsDeleted { get; set; } = false;
    public string? Description { get; private set; }
    public Duration Duration { get; set; }
    public Guid CreatorId { get; private set; }

    public bool IsPublished { get; private set; }
    public DateTime? PublishedOnUtc { get; set; }
    public User Creator { get; set; } = null!;
    
    // List of sections
    private List<Section> _sections { get; set; } = new();
    public IReadOnlyCollection<Section> Sections => _sections;

    private List<Note> _notes { get; set; } = new();
    public IReadOnlyCollection<Note> Notes => _notes;

    private List<Question> _questions { get; set; } = new();
    public IReadOnlyCollection<Question> Questions => _questions;

    

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
        : base(id)
    {
        Url = url;
        Name = name;
        Description = description;
        Duration = duration;
        CreatorId = creatorId;
        IsPublished = false;
        
        RaiseDomainEvent(new VideoCreatedDomainEvent(id));
    }

    public void PublishVideo(DateTime publishedDate)
    {
        IsPublished = true;
        PublishedOnUtc = publishedDate;
        
        RaiseDomainEvent(new PublishedVideoDomainEvent(Id));
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

    public ErrorOr<Note> AddNote(
        Guid id,
        string name,
        string content,
        int time)
    {
        var videoTime = Duration.Create(time);

        if (videoTime.IsError)
        {
            return videoTime.Errors;
        }
        
        // Check if there is any other item on the same time
        if (GetAllItems().Any(n => n.Time.Value == videoTime.Value))
        {
            return CustomErrors.Video.ItemExistsOnThatTime;
        }
        
        // Check if the note is greater than the video duration
        if (videoTime.Value > Duration.Value)
        {
            return CustomErrors.Video.ItemIsGreaterThanVideoDuration;
        }
        
        var note = Note.Create(id, name, content, videoTime.Value, Id);

        if(note.IsError)
        {
            return note.Errors;
        }
        _notes.Add(note.Value);

        return note;
    }

    public ErrorOr<Question> AddQuestion(
        Guid id,
        string name,
        string? feedback,
        string content,
        int time,
        Guid videoId)
    {
        var questionTime = Duration.Create(time);

        if (questionTime.IsError) return questionTime.Errors;

        if (GetAllItems().Any(it => it.Time.Value == questionTime.Value))
            return CustomErrors.Video.ItemExistsOnThatTime;

        var question = Question.Create(
            id,
            name,
            feedback,
            content,
            questionTime.Value,
            videoId);

        if (question.IsError) return question.Errors;
        
        _questions.Add(question.Value);

        return question;

    }

    public IReadOnlyCollection<Item> GetAllItems()
    {
        var items = new List<Item>();
        items.AddRange(_notes);
        items.AddRange(_questions);
        return items.OrderBy(it => it.Time.Value).ToList();
    }
    
    // Set Sections for dapper
    public void SetSections(IEnumerable<Section> sections)
    {
        _sections.AddRange(sections);
    }
    
    // Set Notes for dapper
    public void SetNotes(IEnumerable<Note> notes)
    {
        _notes.AddRange(notes);
    }
    
    //Set questions for dapper
    public void SetQuestions(IEnumerable<Question> questions)
    {
        _questions.AddRange(questions);
    }


}