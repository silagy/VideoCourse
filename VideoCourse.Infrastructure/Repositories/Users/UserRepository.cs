using ErrorOr;
using Microsoft.EntityFrameworkCore;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;
using VideoCourse.Infrastructure.Common;
using VideoCourse.Infrastructure.Common.Queries;
using VideoCourse.Infrastructure.Specifications;

namespace VideoCourse.Infrastructure.Repositories.Users;

public class UserRepository: GenericRepository<User>, IUserRepository
{
    
    public UserRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ErrorOr<User>> Create(User user)
    {
        //Check if user doesn't exists
        if (_dbContext.Set<User>().Any(u => u.Email.Value.ToLower() == user.Email.Value.ToLower()))
        {
            return CustomErrors.User.UserExists;
        }
        // Create user
        await _dbContext.Insert(user);
        return user;
    }

    public Task<ErrorOr<User?>> GetByIdAsync(Guid id)
    {
        return _dbContext.GetByIdAsync<User>(id);
    }

    public async Task<ErrorOr<User?>> GetByIdWithRolesAsync(Guid id)
    {
        User? user = await _dbContext.Set<User>()
            .AddSpecification(new GetUserByIdWithRoles(id))
            .FirstOrDefaultAsync();

        if (user is null) return CustomErrors.User.UserNotFound;

        return user;
    }

    public async Task<ErrorOr<User>> GetByEmailAsync(Email email)
    {
        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email.Value == email.Value);

        if (user is null)
        {
            return CustomErrors.User.UserNotFound;
        }

        return user;
    }

    public Task<ErrorOr<bool>> IsEmailUniqueAsync(Email email)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetUsers(int page, int pageSize, UserRole? role = null)
    {
        IEnumerable<User> response = await _dbContext.GetRecordsUsingRawSqlAsync<User>(
            query: QueriesRepository.Users.GetUsersByRoleId,
            new {RoleId = role, Page = (page - 1) * pageSize, 
                PageSize = pageSize });

        return response;
    }

    public async Task<IEnumerable<Role>> GetRolesById(IEnumerable<UserRole> roles)
    {
        var rolesToReturn = _dbContext.SetNoEntity<Role>().Where(r => roles.Select(role => (int)role).Contains(r.Id));
        return await Task.FromResult(rolesToReturn);
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        IEnumerable<Role> response = await _dbContext.GetRecordsUsingRawSqlAsync<Role>(
            query: QueriesRepository.Roles.GetAllRoles,
            parameters: new { });

        return response;
    }

    public async Task<IEnumerable<Role>> GetRolesByIds(List<int> Ids)
    {
        IEnumerable<Role> response = await _dbContext.GetRecordsUsingRawSqlAsync<Role>(
            query: QueriesRepository.Roles.GetRolesByIds,
            parameters: new { Ids = Ids.ToArray()});

        return response;
    }

    public async Task<IEnumerable<User>> GetCreators()
    {
        IEnumerable<User> response = await _dbContext.GetRecordsUsingRawSqlAsync<User>(
            query: QueriesRepository.Users.GetCreators,
            parameters: new { });

        return response;
    }

    public async Task<int> GetTotalUsers()
    {
        return await _dbContext.Set<User>().CountAsync();
    }

    public async Task<HashSet<string>> GetUserPermissionAsync(Guid userId)
    {
        HashSet<string> permissions = _dbContext.Set<User>()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .SelectMany(r => r.Roles)
            .SelectMany(p => p.Permissions)
            .Select(p => p.Name)
            .ToHashSet();

        return await Task.FromResult(permissions);
    }
}