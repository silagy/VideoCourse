using System.Linq.Expressions;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public abstract class Specification<TEntity> : ISpecification<TEntity>
where TEntity : Entity
{
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    internal bool IsSatisfiedBy(TEntity entity) => ToExpression().Compile()(entity);

    public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification) =>
        specification.ToExpression();
}