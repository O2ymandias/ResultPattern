using ResultPattern.Core.Shared;

namespace ResultPattern.Core.Results;

public class PagedResult<T>(IReadOnlyList<T> data, int page, int pagesSize, int totalCount)
    : PaginationMetadata(page, pagesSize, totalCount)
{
    public IReadOnlyList<T> Data { get; set; } = data;
}