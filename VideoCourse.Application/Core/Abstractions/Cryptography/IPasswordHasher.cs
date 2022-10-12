namespace VideoCourse.Application.Core.Abstractions.Cryptography;

public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the specified password.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <returns>The password hash.</returns>
    string HashPassword(string password);
}