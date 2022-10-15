using System.Data;
using Dapper;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Infrastructure.Common.DapperSqlCasting;

public class VideoUrlHandler : SqlMapper.TypeHandler<VideoUrl>
{
    public override void SetValue(IDbDataParameter parameter, VideoUrl value)
    {
        parameter.Value = value.Value;
    }

    public override VideoUrl Parse(object value)
    {
        var videoUrl = VideoUrl.Create(value.ToString());

        return videoUrl.Value;
    }
}