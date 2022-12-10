using ErrorOr;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Primitives;
using VideoCourse.Domain.Shared;

namespace VideoCourse.Domain.ValueObjects;

public sealed class VideoUrl : ValueObject
{
    private const int MinLength = 3;

    
    private VideoUrl(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static ErrorOr<VideoUrl> Create(string value)
    {
        // Check if value is valid URL
        ErrorOr<string> url = value;
        return url
            .Ensure(u => Uri.IsWellFormedUriString(u, UriKind.RelativeOrAbsolute),
                CustomErrors.Url.NotValidUrl)
            .Ensure(u => u.Length >= MinLength,
                CustomErrors.Url.TooShort(MinLength))
            .Map(u => new VideoUrl(u));
        
    }

    private protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(VideoUrl url) => url.Value;
}