using FluentValidation;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        Include(new PaginatedQueryValidator());
    }
}