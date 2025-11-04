using ResultPattern.Core.Errors;

namespace ResultPattern.Api.Responses;

public class ApiResponse<T>
{
    private ApiResponse(bool success, T? data, Error? error, DateTime timestamp)
    {
        Success = success;
        Data = data;
        Error = error;
        Timestamp = timestamp;
    }

    public bool Success { get; set; }
    public T? Data { get; set; }
    public Error? Error { get; set; }
    public DateTime Timestamp { get; set; }

    public static ApiResponse<T> SuccessResponse(T data)
    {
        return new ApiResponse<T>(true, data, null, DateTime.UtcNow);
    }

    public static ApiResponse<T> ErrorResponse(Error error)
    {
        return new ApiResponse<T>(false, default, error, DateTime.UtcNow);
    }
}