using ErrorOr;
using Microsoft.Extensions.Caching.Memory;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;
using VideoCourse.Infrastructure.Common;

namespace VideoCourse.Infrastructure.Repositories.Users;

public class CachedUserRepository : IUserRepository
{
    private readonly UserRepository _decorator;
    private readonly IMemoryCache _memoryCache;

    public CachedUserRepository(UserRepository decorator, IMemoryCache memoryCache)
    {
        _decorator = decorator;
        _memoryCache = memoryCache;
    }

    public Task<bool> Remove(User entity) => _decorator.Remove(entity);

    public Task<ErrorOr<User>> Add(User entity) => _decorator.Add(entity);

    public Task<IEnumerable<User>> All() => _decorator.All();

    public Task<IEnumerable<TEntity>> GetRecordsWithSpecification<TEntity>(ISpecification<TEntity> specification)
        where TEntity : Entity
        => _decorator.GetRecordsWithSpecification(specification);

    public Task<ErrorOr<User>> Create(User user) => _decorator.Create(user);

    public Task<ErrorOr<User?>> GetByIdAsync(Guid id) => _decorator.GetByIdAsync(id);

    public Task<ErrorOr<User>> GetByEmailAsync(Email email) => _decorator.GetByEmailAsync(email);

    public Task<ErrorOr<bool>> IsEmailUniqueAsync(Email email) => _decorator.IsEmailUniqueAsync(email);

    public Task<IEnumerable<User>> GetUsers(int page, int pageSize, UserRole? role = null) =>
        _decorator.GetUsers(page, pageSize, role);

    public Task<IEnumerable<Role>> GetRolesById(IEnumerable<UserRole> roles) => _decorator.GetRolesById(roles);
    
    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        string key = $"roles";

        var result = await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                return _decorator.GetAllRoles();
            });

        return result ?? new List<Role>();
    }

    public Task<IEnumerable<User>> GetCreators() => _decorator.GetCreators();

    public Task<int> GetTotalUsers() => _decorator.GetTotalUsers();

    public async Task<HashSet<string>> GetUserPermissionAsync(Guid userId)
    {
        string key = $"user-permissions-{userId}";

        var result = await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                return _decorator.GetUserPermissionAsync(userId);
            });

        return result ?? new HashSet<string>();
    }
}