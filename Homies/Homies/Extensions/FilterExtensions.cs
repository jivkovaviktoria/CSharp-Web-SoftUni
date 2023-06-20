using System.Linq.Expressions;

namespace Homies.Services.Extensions;

public static class FilterExtensions
{
    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> collection, Expression<Func<TEntity, bool>>? filter)
    {
        if (filter is null) return collection;
        return collection.Where(filter);
    }
}