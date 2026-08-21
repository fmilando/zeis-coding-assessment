namespace Zeiss.Products.WebApi.Contracts;

public sealed record ApiResponse<T>(
    bool Success,
    T? Data,
    ApiMessage[]? Messages,
    object Metadata);