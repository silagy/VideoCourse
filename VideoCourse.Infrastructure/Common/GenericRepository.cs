using Microsoft.EntityFrameworkCore;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Common;

public class GenericRepository<T> : IRepository<T>
where T : Entity
{
    protected readonly IDbContext _dbContext;

    public GenericRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public bool Remove(T entity)
    {
        _dbContext.Remove(entity);
        return true;
    }

    public async Task<T> Add(T entity)
    {
        await _dbContext.Insert(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> All()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
}