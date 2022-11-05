using Microsoft.EntityFrameworkCore;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> AddSpecification<TEntity>(this IQueryable<TEntity> queryable,
        Specification<TEntity> specification)
        where TEntity : Entity
    {
        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        queryable = specification.Includes.Aggregate(
            queryable,
            (current, includeExpression) =>
                current.Include(includeExpression));

        if (specification.OrderByCriteria is not null)
        {
            queryable = queryable.OrderBy(specification.OrderByCriteria);
        }

        if (specification.OrderByDescendingCriteria is not null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingCriteria);
        }

        return queryable;
    }
}