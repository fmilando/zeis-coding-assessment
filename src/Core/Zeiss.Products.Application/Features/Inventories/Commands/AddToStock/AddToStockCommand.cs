using MediatR;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Inventories.Commands.AddToStock;

public sealed record AddToStockCommand(
    int ProductId,
    int Quantity
) : IRequest<Result<ProductInventoryReadModel>>;