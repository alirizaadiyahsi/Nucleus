namespace Nucleus.Utilities.Collections.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> PagedBy<T>(
        this IEnumerable<T> source,
        int pageIndex,
        int pageSize)
    {
        return source.Skip(pageIndex * pageSize).Take(pageSize);
    }

    public static IPagedListResult<T> ToPagedListResult<T>(
        this IEnumerable<T> source,
        int count)
    {
        var pagedList = new PagedListResult<T>
        {
            TotalCount = count,
            Items = source
        };

        return pagedList;
    }
}