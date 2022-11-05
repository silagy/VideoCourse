using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public class GetVideoByIdWithCreator : Specification<Video>
{

    public GetVideoByIdWithCreator(Guid id)
    :base(video => video.Id == id)
    {
        Includes.Add(v => v.Creator);
    }
    
}