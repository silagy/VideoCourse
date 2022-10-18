using ErrorOr;
using Microsoft.EntityFrameworkCore;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.ValueObjects;
using VideoCourse.Infrastructure.Common;

namespace VideoCourse.Infrastructure.Repositories;

public class UserRepository: GenericRepository<User>, IUserRepository
{
    
    public UserRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ErrorOr<User>> Create(User user)
    {
        //Check if user doesn't exists
        if (_dbContext.Set<User>().Any(u => u.Email.Value == user.Email.Value))
        {
            return CustomErrors.User.UserExists;
        }
        // Create user
        _dbContext.Insert(user);
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
}