﻿using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Queries.GetVidoesByCreatorId;

public class GetVideosByCreatorIdQueryHandler : IRequestHandler<GetVideosByCreatorIdQuery, IEnumerable<BasicVideoResponse>>
{
    private readonly IVideoRepository _videoRepository;

    public GetVideosByCreatorIdQueryHandler(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async Task<IEnumerable<BasicVideoResponse>> Handle(GetVideosByCreatorIdQuery request, CancellationToken cancellationToken)
    {
        // Get the records
        var results = await _videoRepository.GetVideosByCreatorId(request.Id);

        return results.Adapt<IEnumerable<BasicVideoResponse>>();
    }
}