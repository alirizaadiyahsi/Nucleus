namespace Nucleus.Utilities.Collections;

public interface IPagedListResult<T>
{
    int TotalCount { get; set; }

    IEnumerable<T> Items { get; set; }
}