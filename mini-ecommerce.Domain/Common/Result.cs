namespace mini_ecommerce.Domain.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public T Value { get; }

    private Result(bool success, T value, string error)
    {
        IsSuccess = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value)
        => new(true, value, string.Empty);

    public static Result<T> Failure(string error)
        => new(false, default!, error);
}