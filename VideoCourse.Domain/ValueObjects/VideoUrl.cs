using ErrorOr;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Domain.ValueObjects;

public sealed class VideoUrl : ValueObject
{
    public const int MinLength = 3;
    private VideoUrl(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static ErrorOr<VideoUrl> Create(string value)
    {
        // Check if value is valid URL
        if (!Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
        {
            return Error.Validation(
                code: "Value.IsNotUrl",
                description: "The provided value is not URL");
        }

        if (value.Length < MinLength)
        {
            return Error.Validation(
                code: "VideoUrl.IsToShort",
                description: $"The video url must be more then {MinLength}");
        }

        return new VideoUrl(value);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(VideoUrl url) => url.Value;
}