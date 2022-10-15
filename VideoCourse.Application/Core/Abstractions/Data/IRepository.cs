using ErrorOr;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Data;

public interface IRepository<T>
where T : Entity
{
    Task<bool> Remove(T entity);
    Task<ErrorOr<T>> Add(T entity);
    Task<IEnumerable<T>> All();
    Task<IEnumerable<TEntity>> GetRecordsWithSpecification<TEntity>(ISpecification<TEntity> specification)
        where TEntity : Entity;
}