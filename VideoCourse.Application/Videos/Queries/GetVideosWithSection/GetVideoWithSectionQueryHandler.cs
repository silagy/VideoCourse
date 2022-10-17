using ErrorOr;
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
        var videoResponse = await _videoRepository.GetByIdWithSections(request.Id);

        if (videoResponse.IsError)
        {
            return videoResponse.Errors;
        }

        var video = videoResponse.Value;
        // Need to transform to video response
        return new VideoResponse(
            video.Id,
            video.Url.Value,
            video.Name,
            video.Description,
            video.Duration.Value,
            video.CreatorId,
            video.CreationDate,
            video.UpdateDate,
            Sections: video.Sections.Select(s => new SectionResponse(
                s.Id,
                s.Name,
                s.Description,
                s.StartTime.Value,
                s.EndTime.Value,
                s.CreationDate,
                s.UpdateDate)),
            Items: video.GetAllItems().Select(it => new ItemResponse(
                it.Id,
                it.Name,
                it.Content,
                it.Time.Value,
                it.VideoId,
                it.CreationDate,
                it.UpdateDate))
        );
    }
}