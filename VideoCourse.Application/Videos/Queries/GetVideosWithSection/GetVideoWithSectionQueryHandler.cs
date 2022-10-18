using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Videos.Queries.GetVideosWithSection;

public class GetVideoWithSectionQueryHandler : IRequestHandler<GetVideoWithSectionsQuery, ErrorOr<VideoResponse>>
{
    private readonly IVideoRepository _videoRepository;

    public GetVideoWithSectionQueryHandler(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async Task<ErrorOr<VideoResponse>> Handle(GetVideoWithSectionsQuery request, CancellationToken cancellationToken)
    {
        var videoResponse = await _videoRepository.GetVideoWithAllContentById(request.Id);

        if (videoResponse.IsError)
        {
            return videoResponse.Errors;
        }

        var video = videoResponse.Value;
        // Need to transform to video response
        return video.Adapt<VideoResponse>();
    }
}