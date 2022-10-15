using System.Linq.Expressions;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Data;

public interface ISpecification<TEntity>
where TEntity : Entity
{
    Expression<Func<TEntity, bool>> ToExpression();
}