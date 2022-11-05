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

namespace VideoCourse.Infrastructure.Repositories;

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
            new { RoleId = role, 
                Page = (page - 1) * pageSize, 
                PageSize = pageSize });

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
}