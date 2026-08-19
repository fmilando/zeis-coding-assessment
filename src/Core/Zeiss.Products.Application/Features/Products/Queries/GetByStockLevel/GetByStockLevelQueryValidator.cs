using FluentValidation;

namespace Zeiss.Products.Application.Features.Products.Queries.GetByStockLevel;

internal sealed class GetByStockLevelQueryValidator : AbstractValidator<GetByStockLevelQuery>
{
    public GetByStockLevelQueryValidator()
    {
        RuleFor(x => x.MinQuantity)
            .Must(min => min is null or >= 0)
            .WithName(nameof(GetByStockLevelQuery.MinQuantity));

        RuleFor(x => x.MaxQuantity)
            .Must(max => max is null or >= 0)
            .WithName(nameof(GetByStockLevelQuery.MaxQuantity));

        RuleFor(x => new { x.MinQuantity, x.MaxQuantity })
            .Must(x => (x.MinQuantity ?? x.MaxQuantity) is not null)
            .WithMessage("Either MinQuantity or MaxQuantity or both must be provided");
    }
}