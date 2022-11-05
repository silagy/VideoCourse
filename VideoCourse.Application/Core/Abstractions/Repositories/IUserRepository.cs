using ErrorOr;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Users.Common;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Application.Core.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<ErrorOr<User>> Create(User user);
    Task<ErrorOr<User?>> GetByIdAsync(Guid id);
    Task<ErrorOr<User>> GetByEmailAsync(Email email);
    Task<ErrorOr<bool>> IsEmailUniqueAsync(Email email);
    Task<IEnumerable<User>> GetUsers(int page, int pageSize, UserRole? role = null);
    Task<IEnumerable<User>> GetCreators();
    Task<int> GetTotalUsers();
}