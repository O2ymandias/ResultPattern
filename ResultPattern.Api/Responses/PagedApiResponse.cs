using ResultPattern.Core.Errors;
using ResultPattern.Core.Shared;

namespace ResultPattern.Api.Responses;

public class PagedApiResponse<T> : ApiResponse<IReadOnlyList<T>>
{
    private PagedApiResponse(bool success, IReadOnlyList<T>? data, PaginationMetadata? pagination, Error? error,
        DateTime timestamp) : base(success, data, error, timestamp)
    {
        Pagination = pagination;
    }

    public PaginationMetadata? Pagination { get; set; }


    public static PagedApiResponse<T> SuccessResponse(IReadOnlyList<T> data, PaginationMetadata pagination)
    {
        return new PagedApiResponse<T>(true, data, pagination, null, DateTime.UtcNow);
    }

    public new static PagedApiResponse<T> ErrorResponse(Error error)
    {
        return new PagedApiResponse<T>(false, null, null, error, DateTime.UtcNow);
    }
}