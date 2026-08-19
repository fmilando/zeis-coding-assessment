using Zeiss.Products.Application.Features.Products.Queries;

namespace Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;

public sealed record DecrementStockResult(ProductInventoryReadModel Product);
