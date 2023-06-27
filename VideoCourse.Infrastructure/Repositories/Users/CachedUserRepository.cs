using ErrorOr;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;
using VideoCourse.Infrastructure.Common;

namespace VideoCourse.Infrastructure.Repositories.Users;

public class CachedUserRepository : IUserRepository
{
    private readonly IUserRepository _decorator;
    private readonly IDistributedCache _distributedCache;

    public CachedUserRepository(IUserRepository decorator, IDistributedCache distributedCache)
    {
        _decorator = decorator;
        _distributedCache = distributedCache;
    }

    public Task<bool> Remove(User entity) => _decorator.Remove(entity);

    public Task<ErrorOr<User>> Add(User entity) => _decorator.Add(entity);

    public Task<IEnumerable<User>> All() => _decorator.All();

    public Task<IEnumerable<TEntity>> GetRecordsWithSpecification<TEntity>(ISpecification<TEntity> specification)
        where TEntity : Entity
        => _decorator.GetRecordsWithSpecification(specification);

    public Task<ErrorOr<User>> Create(User user) => _decorator.Create(user);

    public Task<ErrorOr<User?>> GetByIdAsync(Guid id) => _decorator.GetByIdAsync(id);
    public Task<ErrorOr<User?>> GetByIdWithRolesAsync(Guid id) => _decorator.GetByIdWithRolesAsync(id);

    public Task<ErrorOr<User>> GetByEmailAsync(Email email) => _decorator.GetByEmailAsync(email);

    public Task<ErrorOr<bool>> IsEmailUniqueAsync(Email email) => _decorator.IsEmailUniqueAsync(email);

    public Task<IEnumerable<User>> GetUsers(int page, int pageSize, UserRole? role = null) =>
        _decorator.GetUsers(page, pageSize, role);

    public Task<IEnumerable<Role>> GetRolesById(IEnumerable<UserRole> roles) => _decorator.GetRolesById(roles);

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        string key = $"roles";
        IEnumerable<Role> rolesData;
        string? cachedValue = await _distributedCache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cachedValue))
        {
            var roles = await _decorator.GetAllRoles();

            if (roles.Any())
            {
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(roles));
                return await Task.FromResult(roles);
            }
        }

        return JsonConvert.DeserializeObject<IEnumerable<Role>>(cachedValue);


        return new List<Role>();
    }

    public Task<IEnumerable<Role>> GetRolesByIds(List<int> Ids)
    {
        return _decorator.GetRolesByIds(Ids);
    }

    public Task<IEnumerable<User>> GetCreators() => _decorator.GetCreators();

    public async Task<int> GetTotalUsers()
    {
        var key = $"total-users";

        string? cachedKey = await _distributedCache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cachedKey))
        {
            int totalUsers = await _decorator.GetTotalUsers();
            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(totalUsers));

            return totalUsers;
        }

        return JsonConvert.DeserializeObject<int>(cachedKey);
    }

    public async Task<HashSet<string>> GetUserPermissionAsync(Guid userId)
    {
        string key = $"user-permissions-{userId}";

        string? userPermissionsKey = await _distributedCache.GetStringAsync(key);

        if (string.IsNullOrEmpty(userPermissionsKey))
        {
            var userPermissions = await _decorator.GetUserPermissionAsync(userId);
            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(userPermissions));

            return userPermissions;
        }
        else
        {
            var cachedUserPermissions =
                JsonConvert.DeserializeObject<HashSet<string>>(await _distributedCache.GetStringAsync(key));
            return cachedUserPermissions;
        }

        return new HashSet<string>();
    }
}