using ResultPattern.Core.Shared;

namespace ResultPattern.Core.Results;

public class PagedResult<T>(IReadOnlyList<T> items, PaginationMetadata pagination)
{
    public IReadOnlyList<T> Items { get; set; } = items;
    public PaginationMetadata Pagination { get; set; } = pagination;
}