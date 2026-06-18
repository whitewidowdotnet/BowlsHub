namespace OakdaleRolbal.Application.Common.Results;

public enum ResultErrorType
{
    Validation,
    Unauthorized,
    NotFound
}

public sealed class Result<T>
{
    private Result(
        bool isSuccess,
        T? value,
        ResultErrorType? errorType,
        string? errorMessage,
        Dictionary<string, string[]>? validationErrors)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorType = errorType;
        ErrorMessage = errorMessage;
        ValidationErrors = validationErrors;
    }

    public bool IsSuccess { get; }

    public T? Value { get; }

    public ResultErrorType? ErrorType { get; }

    public string? ErrorMessage { get; }

    public Dictionary<string, string[]>? ValidationErrors { get; }

    public static Result<T> Success(T value) => new(true, value, null, null, null);

    public static Result<T> Validation(Dictionary<string, string[]> validationErrors) =>
        new(false, default, ResultErrorType.Validation, null, validationErrors);

    public static Result<T> Unauthorized(string errorMessage) =>
        new(false, default, ResultErrorType.Unauthorized, errorMessage, null);

    public static Result<T> NotFound(string errorMessage) =>
        new(false, default, ResultErrorType.NotFound, errorMessage, null);
}
