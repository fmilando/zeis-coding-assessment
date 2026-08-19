namespace Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    long ProductId,
    string Name,
    string Sku,
    string? Description,
    decimal Price
) : BaseCommand(ProductId);