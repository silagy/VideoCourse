using System.Linq.Expressions;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public class GetPublishedVideosByCreatorId : Specification<Video>
{
    public GetPublishedVideosByCreatorId(Guid creatorId)
    {
        CreatorId = creatorId;
    }

    private Guid CreatorId { get; set; }

    public override Expression<Func<Video, bool>> ToExpression() =>
        video => video.IsPublished == true && video.CreatorId == CreatorId;
}