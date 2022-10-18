using ErrorOr;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class Question : Item
{
    public string? Feedback { get; private set; }
    public int QuestionTypeId { get; private set; }

    private List<QuestionOption> _questionOptions { get; set; } = new();
    public IReadOnlyCollection<QuestionOption> QuestionOptions => _questionOptions;

    public QuestionType QuestionType => (QuestionType)TypeId;

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
        string? feedback,
        QuestionType questionType) 
        : base(id, name, content, time, videoId)
    {
        Feedback = feedback;
        TypeId = (int)ItemType.Question;
        QuestionTypeId = (int)questionType;
    }

    public static ErrorOr<Question> Create(
        Guid id,
        string name,
        string? feedback,
        string content,
        int time,
        Guid videoId,
        QuestionType questionType
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
            feedback,
            questionType
            );
    }

    public ErrorOr<QuestionOption> AddOption(
        Guid id,
        string name,
        bool isRight)
    {
        var option = new QuestionOption(
            id,
            name,
            isRight,
            Id);

        if (
            option.IsRight
            && QuestionType == QuestionType.MultipleAnswersSingleSelection
            && QuestionOptions.Any(q => q.IsRight)
            )
            return CustomErrors.Question.MultipleAnswersSingleSelectionMustHaveOneRightAnswer;
        
        _questionOptions.Add(option);

        return option;
    }
}