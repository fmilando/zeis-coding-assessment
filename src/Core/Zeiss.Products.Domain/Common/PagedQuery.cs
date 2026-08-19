namespace Zeiss.Products.Domain.Common;

public record PagedQuery
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}