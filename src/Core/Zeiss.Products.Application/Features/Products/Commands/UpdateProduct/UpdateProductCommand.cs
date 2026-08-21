using MediatR;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    int ProductId,
    string Name,
    string Sku,
    string? Description,
    decimal Price
) : IRequest<Result<ProductInventoryReadModel>>;