namespace IndexMe.Domain.Results;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public string? LogMessage { get; }

    protected Result(bool isSuccess, string error, string? logMessage)
    {
        IsSuccess = isSuccess;
        Error = error;
        LogMessage = logMessage;
    }

    public static Result Success(string? logMessage = null) => new(true, string.Empty, logMessage);
    public static Result Failure(string error, string? logMessage = null) => new(false, error, logMessage);
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(T value, bool isSuccess, string error, string? logMessage = null) : base(isSuccess, error, logMessage)
    {
        Value = value;
    }

    public static Result<T> Success(T value, string? logMessage = null) => new(value, true, string.Empty, logMessage);
}
