using FluentValidation;
using Zeiss.Products.Domain.Constants;

namespace Zeiss.Products.Application.Features.Inventories.Commands.AddToStock;

internal sealed class AddToStockCommandValidator : AbstractValidator<AddToStockCommand>
{
    public AddToStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThanOrEqualTo(ProductConstants.IdStartValue)
            .LessThanOrEqualTo(ProductConstants.IdMaxValue)
            .WithName(nameof(AddToStockCommand.ProductId));
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithName(nameof(AddToStockCommand.Quantity));
    }
}