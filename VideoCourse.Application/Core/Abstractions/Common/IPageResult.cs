namespace VideoCourse.Application.Core.Abstractions.Common;

public interface IPageResult<TEntity>
{
    int TotalRecords { get; set; }
    int Page { get; set; }
    int PageSize { get; set; }
    IReadOnlyCollection<TEntity> Items { get; }
}