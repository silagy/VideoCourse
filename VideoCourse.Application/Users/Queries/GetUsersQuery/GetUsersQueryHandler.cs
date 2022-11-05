using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Core.Common;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Users.Queries.GetUsersQuery;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<PageResult<BasicUserResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<PageResult<BasicUserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        // Get the records
        IEnumerable<User> users = await _userRepository.GetUsers(request.Page, request.PageSize, request.Role);

        int totalRecords = await _userRepository.GetTotalUsers();

        if (request.SearchTerm is not null)
        {
            users = users.Where(u =>
                u.FirstName.Contains(request.SearchTerm) ||
                u.LastName.Contains(request.SearchTerm));
        }

        return new PageResult<BasicUserResponse>(
            totalRecords,
            request.Page,
            request.PageSize,
            users.Adapt<IReadOnlyCollection<BasicUserResponse>>());
    }
}