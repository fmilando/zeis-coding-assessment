namespace Zeiss.Products.Application.Results;

public sealed class Result<T>
{
    private Result(T value) => Value = value;
    private Result(IReadOnlyCollection<Error> errors) => Errors = errors;
    private Result(T value, IReadOnlyCollection<Error> errors)
    {
        Value = value;
        Errors = errors;
    }

    public bool IsSuccess => Errors.Count is 0;
    public bool IsError => IsSuccess is false;

    public T? Value { get; }
    public IReadOnlyCollection<Error> Errors { get; } = [];

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error[] errors) => new(errors);
    public static implicit operator Result<T>(Error error) => new([error]);
    public static implicit operator Result<T>((T Value, Error Error) data) => new(data.Value, [data.Error]);
}