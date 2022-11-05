using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Core.Common;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Queries.GetVideos;

public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, ErrorOr<PageResult<BasicVideoResponse>>>
{
    private readonly IVideoRepository _videoRepository;

    public GetVideosQueryHandler(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async Task<ErrorOr<PageResult<BasicVideoResponse>>> Handle(GetVideosQuery request, CancellationToken cancellationToken)
    {
        var videos = await _videoRepository.GetVideosByParameters(
            request.CreatorId,
            request.StartDate,
            request.EndDate,
            request.Page,
            request.PageSize);

        var totalVideos = await _videoRepository.GetTotalVideos();

        if (request.SearchTerm is not null)
        {
            videos = videos.Where(v => v.Name.Contains(request.SearchTerm));
        }

        return new PageResult<BasicVideoResponse>(
            totalVideos ?? 0,
            request.Page,
            request.PageSize,
            videos.Adapt<IReadOnlyCollection<BasicVideoResponse>>());
    }
}