using ResultPattern.Core.Errors;
using ResultPattern.Core.Shared;

namespace ResultPattern.Api.Responses;

public class PagedApiResponse<T>
{
    private PagedApiResponse(bool success, IReadOnlyList<T>? data, PaginationMetadata? pagination, Error? error,
        DateTime timestamp)
    {
        Success = success;
        Data = data;
        Pagination = pagination;
        Error = error;
        Timestamp = timestamp;
    }

    public bool Success { get; set; }
    public IReadOnlyList<T>? Data { get; set; }
    public PaginationMetadata? Pagination { get; set; }
    public Error? Error { get; set; }
    public DateTime Timestamp { get; set; }

    public static PagedApiResponse<T> SuccessResponse(IReadOnlyList<T> data, PaginationMetadata pagination)
    {
        return new PagedApiResponse<T>(true, data, pagination, null, DateTime.UtcNow);
    }

    public static PagedApiResponse<T> ErrorResponse(Error error)
    {
        return new PagedApiResponse<T>(false, null, null, error, DateTime.UtcNow);
    }
}