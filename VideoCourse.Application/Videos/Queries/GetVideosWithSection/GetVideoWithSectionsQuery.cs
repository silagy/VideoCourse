using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Queries.GetVideosWithSection;

public record GetVideoWithSectionsQuery(Guid Id): IRequest<ErrorOr<VideoResponse>>;