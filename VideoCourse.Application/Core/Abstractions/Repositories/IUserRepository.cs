using ErrorOr;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<ErrorOr<User>> Create(User user);
    Task<ErrorOr<User>> GetByIdAsync(Guid id);
    Task<ErrorOr<User>> GetByEmailAsync(Email email);
    Task<ErrorOr<bool>> IsEmailUniqueAsync(Email email);
}