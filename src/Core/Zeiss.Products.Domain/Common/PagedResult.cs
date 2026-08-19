namespace Zeiss.Products.Domain.Common;

public record PagedResult<T>(
    T Result,
    PaginationInfo PaginationInfo)
    where T : class;