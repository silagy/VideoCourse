namespace VideoCourse.Application.Core.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid id, string firstname, string lastname);
}