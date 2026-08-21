using MediatR;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Results;

namespace Zeiss.Products.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int? Quantity) : IRequest<Result<ProductInventoryReadModel>>;