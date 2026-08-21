using Zeiss.Products.Domain.Extensions;

namespace Zeiss.Products.Domain.Common;

public sealed record PaginationInfo(
    int? PageNumber,
    int? PageSize,
    int? TotalPages,
    int? TotalItems
)
{
    public string Timestamp { get; } = DateTime.UtcNow.ToIso8601();
};