namespace Zeiss.Products.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Sku,
    string? Description,
    decimal Price);