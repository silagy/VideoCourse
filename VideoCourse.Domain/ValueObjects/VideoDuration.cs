using ErrorOr;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Primitives;
using VideoCourse.Domain.Shared;

namespace VideoCourse.Domain.ValueObjects;

public sealed class Duration : ValueObject
{
    public const int MinDuration = 0;
    public int Value { get; }

    private Duration(int value)
    {
        Value = value;
    }

    private protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static ErrorOr<Duration> Create(int value)
    {
        ErrorOr<int> duration = value;

        return duration
            .Ensure(d => d >= MinDuration,
                CustomErrors.Duration.DurationIsNotPositive(MinDuration))
            .Map(d => new Duration(duration.Value));
    }

    public static ErrorOr<Duration> Create(string value)
    {
        int duration = int.Parse(value);

        return Create(duration);
    }

    public static implicit operator int(Duration duration) => duration.Value;
}