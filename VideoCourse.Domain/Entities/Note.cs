using ErrorOr;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class Note : Item
{
    private Note(){}
    private Note(
        Guid id,
                string name,
                string content,
                Duration time,
                Guid videoId)
    :base(id, name, content, time , videoId)
    {
        TypeId = (int)(ItemType.Note);
    }
    
    public static ErrorOr<Note> Create(
        Guid id,
        string name,
        string content,
        Duration time,
        Guid videoId)
    {
        

        return new Note(
            id,
            name,
            content,
            time,
            videoId);
    }
}