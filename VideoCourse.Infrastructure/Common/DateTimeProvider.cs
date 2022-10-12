using VideoCourse.Application.Core.Abstractions.Common;

namespace VideoCourse.Infrastructure.Common;

public class DateTimeProvider : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}