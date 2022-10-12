using ErrorOr;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Domain.ValueObjects;

public sealed class Duration : ValueObject
{
    public const int MinDuration = 0;
    public int Value { get; }

    private Duration(int value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static ErrorOr<Duration> Create(int value)
    {
        if (value < MinDuration)
        {
            return Error.Validation(
                code: "Video.DurationIsNotPositive",
                description: $"Video duration max be greater than {MinDuration}");
        }

        return new Duration(value);
    }

    public static implicit operator int(Duration duration) => duration.Value;
}