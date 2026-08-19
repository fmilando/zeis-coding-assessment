using FluentValidation;
using Zeiss.Products.Domain.Constants;

namespace Zeiss.Products.Application.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(ProductConstants.NameMaxLength)
            .WithName(nameof(CreateProductCommand.Name));
        
        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(ProductConstants.SkuMaxLength)
            .WithName(nameof(CreateProductCommand.Sku));
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithName(nameof(CreateProductCommand.Price));
        
        RuleFor(x => x.Description)
            .MaximumLength(ProductConstants.DescriptionMaxLength)
            .WithName(nameof(CreateProductCommand.Description));
    }
}