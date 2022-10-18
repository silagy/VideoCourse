using ErrorOr;
using Microsoft.EntityFrameworkCore;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Common;

public abstract class GenericRepository<T> : IRepository<T>
where T : AggregateRoot
{
    protected readonly IDbContext _dbContext;

    public GenericRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<bool> Remove(T entity)
    {
        _dbContext.Remove(entity);
        return Task.FromResult(true);
    }

    public async Task<ErrorOr<T>> Add(T entity)
    {
        var result = await _dbContext.Insert(entity);
        return result;
    }

    public async Task<IEnumerable<T>> All()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetRecordsWithSpecification<TEntity>(ISpecification<TEntity> specification) where TEntity : Entity
    {
        var results = await _dbContext.Set<TEntity>()
            .Where(specification.ToExpression())
            .ToListAsync();

        return results;
    }
}