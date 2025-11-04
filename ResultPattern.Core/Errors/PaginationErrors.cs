namespace ResultPattern.Core.Errors;

public class PaginationErrors
{
    public static Error InvalidPage()
    {
        return Error.Validation("Pagination.InvalidPage", "Page must be greater than 0");
    }

    public static Error InvalidPageSize(int minPageSize, int maxPageSize)
    {
        return Error.Validation("Pagination.InvalidPageSize",
            $"PageSize must be between {minPageSize} and {maxPageSize}");
    }
}