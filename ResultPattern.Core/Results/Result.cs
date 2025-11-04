using ResultPattern.Core.Errors;

namespace ResultPattern.Core.Results;

public class Result
{
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null)
            throw new ArgumentException("Successful result cannot have an error.");

        if (!isSuccess && error is null)
            throw new ArgumentException("Failed result must contain an error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error? Error { get; }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }
}

public class Result<T> : Result
{
    private Result(bool isSuccess, T? value, Error? error) : base(isSuccess, error)
    {
        Value = value;
    }

    public T? Value { get; set; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, null);
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, error);
    }
}