using ErrorOr;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class Question : Item
{
    public string? Feedback { get; private set; }

    private Question(){}
    private Question(string? feedback)
    {
        Feedback = feedback;
    }
    
    private Question(
        Guid id,
        string name,
        string content,
        Duration time,
        Guid videoId, 
        string? feedback) 
        : base(id, name, content, time, videoId)
    {
        Feedback = feedback;
        TypeId = (int)ItemType.Question;
    }

    public static ErrorOr<Question> Create(
        Guid id,
        string name,
        string? feedback,
        string content,
        int time,
        Guid videoId
    )
    {
        var timeObject = Duration.Create(time);

        if (timeObject.IsError)
        {
            return timeObject.Errors;
        }

        return new Question(
            id,
            name,
            content,
            timeObject.Value,
            videoId,
            feedback
            );
    }
}