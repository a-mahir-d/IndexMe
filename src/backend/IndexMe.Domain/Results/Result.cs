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
    public T Data { get; }

    protected Result(T data, bool isSuccess, string error, string? logMessage = null) : base(isSuccess, error, logMessage)
    {
        Data = data;
    }

    public static Result<T> Success(T data, string? logMessage = null) => new(data, true, string.Empty, logMessage);
    public static new Result<T> Failure(string error, string? logMessage = null) => new(default!, false, error, logMessage);
}
