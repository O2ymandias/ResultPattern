using System.Net;

namespace ResultPattern.Core.Errors;

public class Error
{
    private Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    public string Code { get; set; }
    public string Message { get; set; }
    public ErrorType ErrorType { get; set; }

    public static Error NotFound(string code, string message)
    {
        return new Error(code, message, ErrorType.NotFound);
    }

    public static Error Validation(string code, string message)
    {
        return new Error(code, message, ErrorType.Validation);
    }

    public static Error Conflict(string code, string message)
    {
        return new Error(code, message, ErrorType.Conflict);
    }

    public static Error Failure(string code, string message)
    {
        return new Error(code, message, ErrorType.Failure);
    }
}

public enum ErrorType
{
    NotFound = HttpStatusCode.NotFound,
    Validation = HttpStatusCode.BadRequest,
    Conflict = HttpStatusCode.Conflict,
    Failure = HttpStatusCode.InternalServerError
}