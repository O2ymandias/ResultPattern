using ResultPattern.Core.Interfaces;

namespace ResultPattern.Infrastructure;

public class SpecificationsEvaluator
{
    public static IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> specs)
        where TEntity : class
    {
        var query = inputQuery.AsQueryable();

        if (specs.Criteria is not null) query = query.Where(specs.Criteria);

        if (specs.IsPaginationEnabled) query = query.Skip(specs.Skip).Take(specs.Take);

        return query;
    }
}