namespace Zeiss.Products.WebApi.Contracts;

public sealed record ApiResponse<T>(
    bool Success,
    T? Data,
    ApiError[]? Errors,
    object Metadata);