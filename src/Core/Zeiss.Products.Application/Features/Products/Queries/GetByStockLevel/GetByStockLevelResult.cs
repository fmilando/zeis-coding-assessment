using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries.GetByStockLevel;

public sealed record GetByStockLevelResult(
    IReadOnlyCollection<ProductInventoryReadModel> Products,
    PaginationInfo Metadata
);
