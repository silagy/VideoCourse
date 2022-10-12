using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Data;

public interface IRepository<T>
where T : Entity
{
    bool Remove(T entity);
    Task<T> Add(T entity);
    Task<IEnumerable<T>> All();
}