namespace Zeiss.Products.Domain.Common;

public record PagedResult<T>(
    T Result,
    PaginationInfo Metadata
) where T : class;