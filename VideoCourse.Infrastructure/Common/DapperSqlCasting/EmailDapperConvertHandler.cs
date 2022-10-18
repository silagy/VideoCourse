using System.Data;
using Dapper;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Infrastructure.Common.DapperSqlCasting;

public class EmailDapperConvertHandler : SqlMapper.TypeHandler<Email>
{
    public override void SetValue(IDbDataParameter parameter, Email value)
    {
        parameter.Value = value.Value;
    }

    public override Email Parse(object value)
    {
        return Email.Create(value.ToString()!).Value;
    }
}