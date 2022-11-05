using VideoCourse.Application.Core.Abstractions.Common;

namespace VideoCourse.Application.Core.Common;

public class PageResult<T> : IPageResult<T>
{
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyCollection<T> Items { get; }

    public PageResult(
        int totalRecords,
        int page,
        int pageSize,
        IReadOnlyCollection<T> items)
    {
        TotalRecords = totalRecords;
        Page = page;
        PageSize = pageSize;
        Items = items;
    }

    public bool HasNextPage => Page * PageSize < TotalRecords;
    public bool HasPreviousPage => Page > 1;
}