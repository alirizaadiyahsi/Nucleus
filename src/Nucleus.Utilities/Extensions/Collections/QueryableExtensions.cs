using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Nucleus.Utilities.Extensions.Collections
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(
            this IQueryable<TEntity> source,
            bool condition = false,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            if (condition && predicate != null)
            {
                source = source.Where(predicate);
            }

            return source;
        }

        public static IQueryable<T> PagedBy<T>(
            this IQueryable<T> source,
            int pageIndex,
            int pageSize,
            int indexFrom = 0,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            return source.Skip((pageIndex - indexFrom) * pageSize).Take(pageSize);
        }
    }
}
