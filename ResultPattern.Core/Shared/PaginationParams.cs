using ResultPattern.Core.Errors;
using ResultPattern.Core.Results;

namespace ResultPattern.Core.Shared;

public record PaginationParams
{
    private const int MinPage = 1;
    private const int MinPageSize = 5;
    private const int MaxPageSize = 50;

    public int Page { get; set; }
    public int PageSize { get; set; }

    public Result Validate()
    {
        if (Page < MinPage)
            return Result.Failure(PaginationErrors.InvalidPage());

        if (PageSize is < MinPageSize or > MaxPageSize)
            return Result.Failure(PaginationErrors.InvalidPageSize(MinPageSize, MaxPageSize));

        return Result.Success();
    }
}