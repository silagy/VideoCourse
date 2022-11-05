using System.Linq.Expressions;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public abstract class Specification<TEntity>
where TEntity : Entity
{
    protected Specification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
    
    public Expression<Func<TEntity, object>>? OrderByCriteria { get; private set; }
    
    public Expression<Func<TEntity, object>>? OrderByDescendingCriteria { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> include)
    {
        Includes.Add(include);
    }

    protected void OrderBy(Expression<Func<TEntity, object>> orderBy)
    {
        OrderByCriteria = orderBy;
    }

    protected void OrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
    {
        OrderByDescendingCriteria = orderByDescending;
    }
}