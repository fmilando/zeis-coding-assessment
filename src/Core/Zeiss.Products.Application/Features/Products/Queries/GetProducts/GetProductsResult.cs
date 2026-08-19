using Zeiss.Products.Domain.Common;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProducts;

public sealed record GetProductsResult(
    IReadOnlyCollection<ProductInventoryReadModel> Products,
    PaginationInfo Metadata
);
