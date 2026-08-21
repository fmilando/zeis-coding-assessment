using MediatR;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;

public sealed record DecrementStockCommand(
    int ProductId,
    int Quantity
) : IRequest<Result<ProductInventoryReadModel>>;