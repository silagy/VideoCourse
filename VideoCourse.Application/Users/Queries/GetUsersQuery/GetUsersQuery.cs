using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Common;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Application.Users.Queries.GetUsersQuery;

public record GetUsersQuery(
    UserRole? Role,
    string? SearchTerm,
    int Page,
    int PageSize
    ) : IRequest<ErrorOr<PageResult<BasicUserResponse>>>;