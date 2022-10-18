using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public abstract class Item : Entity
{
    public string Name { get; set; }
    
    public string? Content { get; set; }
    public Duration Time { get; set; }
    public int TypeId { get; set; }
    public ItemType Type => (ItemType)TypeId;
    public Guid VideoId { get; set; }

    public Video Video { get; set; }

    protected Item()
    {
    }
    protected Item(Guid id, 
        string name,
        string? content,
        Duration time,
        Guid videoId)
    :base(id)
    {
        Name = name;
        Time = time;
        Content = content;
        VideoId = videoId;
        TypeId = (int)ItemType.Note;
    }
}