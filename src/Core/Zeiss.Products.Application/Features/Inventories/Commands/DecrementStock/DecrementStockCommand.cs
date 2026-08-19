namespace Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;

public sealed record DecrementStockCommand(
    long ProductId,
    int Quantity
) : BaseCommand(ProductId);