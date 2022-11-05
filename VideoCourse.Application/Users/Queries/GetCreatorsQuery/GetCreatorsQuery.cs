using MediatR;
using VideoCourse.Application.Common.Responses;
using VideoCourse.Application.Users.Common;

namespace VideoCourse.Application.Users.Queries.GetCreatorsQuery;

public record GetCreatorsQuery() : IRequest<IEnumerable<FilterResponse>>;