using Mapster;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Videos.Mapping;

public class VideoResponsesMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Video, VideoResponse>()
            .Map(dest => dest.Description, src => src.Duration.Value)
            .Map(dest => dest.Url, src => src.Url.Value)
            .Map(dest => dest.Items, src => src.GetAllItems());

        config.NewConfig<Section, SectionResponse>()
            .Map(dest => dest.StartTime, src => src.StartTime.Value)
            .Map(dest => dest.EndTime, src => src.EndTime.Value);

        config.NewConfig<Item, ItemResponse>()
            .Map(dest => dest.Duration, src => src.Time.Value);

        config.NewConfig<Video, BasicVideoResponse>()
            .Map(dest => dest.Description, src => src.Duration.Value)
            .Map(dest => dest.Url, src => src.Url.Value);

    }
}