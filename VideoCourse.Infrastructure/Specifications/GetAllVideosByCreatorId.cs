using System.Linq.Expressions;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public class GetAllVideosByCreatorId : Specification<Video>
{
    public GetAllVideosByCreatorId(Guid creatorId)
    {
        CreatorId = creatorId;
    }

    private Guid CreatorId { get; set; }

    public override Expression<Func<Video, bool>> ToExpression()
        => v => v.CreatorId == CreatorId;
}