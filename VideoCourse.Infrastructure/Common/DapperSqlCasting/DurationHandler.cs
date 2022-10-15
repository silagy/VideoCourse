using System.Data;
using Dapper;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Infrastructure.Common.DapperSqlCasting;

public class DurationHandler : SqlMapper.TypeHandler<Duration>
{
    public override void SetValue(IDbDataParameter parameter, Duration value)
    {
        parameter.Value = value.Value;
    }

    public override Duration Parse(object value)
    {
        
        var DurationResult = Domain.ValueObjects.Duration.Create(value.ToString());
        return DurationResult.Value;
    }
}