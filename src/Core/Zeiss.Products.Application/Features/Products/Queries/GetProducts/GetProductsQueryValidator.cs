using FluentValidation;

namespace Zeiss.Products.Application.Features.Products.Queries.GetProducts;

public sealed class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        Include(new PaginatedQueryValidator());
    }
}