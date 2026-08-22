using FluentValidation;
using Zeiss.Products.Domain.Constants;

namespace Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThanOrEqualTo(ProductConstants.IdStartValue)
            .LessThanOrEqualTo(ProductConstants.IdMaxValue)
            .WithName(nameof(UpdateProductCommand.ProductId));
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(ProductConstants.NameMaxLength)
            .WithName(nameof(UpdateProductCommand.Name));

        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(ProductConstants.SkuMaxLength)
            .WithName(nameof(UpdateProductCommand.Sku));

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithName(nameof(UpdateProductCommand.Price));

        RuleFor(x => x.Description)
            .MaximumLength(ProductConstants.DescriptionMaxLength)
            .WithName(nameof(UpdateProductCommand.Description));
    }
}