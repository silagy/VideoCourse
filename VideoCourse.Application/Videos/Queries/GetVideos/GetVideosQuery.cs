using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Common;
using VideoCourse.Application.Users.Common;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Queries.GetVideos;

public record GetVideosQuery(
    Guid? CreatorId,
    string? SearchTerm,
    DateTime? StartDate,
    DateTime? EndDate,
    int Page,
    int PageSize
    ) : IRequest<ErrorOr<PageResult<BasicVideoResponse>>>;