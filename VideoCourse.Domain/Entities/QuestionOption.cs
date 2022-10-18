namespace VideoCourse.Domain.Entities;

public class QuestionOption : Entity
{
    public string Name { get; set; } = null!;
    public bool IsRight { get; set; }
    public Guid QuestionId { get; set; }

    private QuestionOption()
    {
    }

    internal QuestionOption(
        Guid id,
        string name,
        bool isRight,
        Guid questionId): base(id)
    {
        Name = name;
        IsRight = isRight;
        QuestionId = questionId;
    }
}