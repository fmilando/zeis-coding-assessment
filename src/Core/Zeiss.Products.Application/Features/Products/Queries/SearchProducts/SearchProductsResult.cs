using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries.SearchProducts;

public sealed record SearchProductsResult(
    IReadOnlyCollection<ProductInventoryReadModel> Products,
    PaginationInfo Metadata
);
