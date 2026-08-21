namespace Zeiss.Products.Domain.Common;

public record PagedQuery
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
}