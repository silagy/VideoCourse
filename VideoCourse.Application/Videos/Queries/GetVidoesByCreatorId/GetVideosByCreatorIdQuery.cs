using MediatR;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Queries.GetVidoesByCreatorId;

public record GetVideosByCreatorIdQuery(Guid Id):IRequest<IEnumerable<BasicVideoResponse>>;