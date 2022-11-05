using Mapster;
using MediatR;
using VideoCourse.Application.Common.Responses;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Users.Common;

namespace VideoCourse.Application.Users.Queries.GetCreatorsQuery;

public class GetCreatorsQueryHandler : IRequestHandler<GetCreatorsQuery, IEnumerable<FilterResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetCreatorsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<FilterResponse>> Handle(GetCreatorsQuery request, CancellationToken cancellationToken)
    {
        var response = await _userRepository.GetCreators();

        return response.Select(u => new FilterResponse(u.Id, $"{u.FirstName} {u.LastName}"));
    }
}