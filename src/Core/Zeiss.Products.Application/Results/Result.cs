namespace Zeiss.Products.Application.Results;

public sealed class Result<T>
{
    public Result(T value) => Value = value;
    public Result(IReadOnlyCollection<Error> errors) => Errors = errors;

    public bool IsSuccess => Value is not null;
    public bool IsError => IsSuccess is false;
    
    public T? Value { get; }
    public IReadOnlyCollection<Error> Errors { get; } = [];
    
    public static implicit operator Result<T>(T value) => new (value);
    public static implicit operator Result<T>(Error[] errors) => new (errors);
}