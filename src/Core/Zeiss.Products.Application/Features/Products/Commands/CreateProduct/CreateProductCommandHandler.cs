using Zeiss.Products.Application.Interfaces.Handlers;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository products,
    IEventPublisher publisher
) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<Result<CreateProductResult>> HandleAsync(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await products.GetBySkuAsync(request.Sku, cancellationToken);

        if (product is not null)
        {
            var error = new Error(
                ErrorCodes.ProductSkuConflict,
                $"Product {request.Sku} already assigned to a product");
            return new Result<CreateProductResult>([error]);
        }

        product = new Product(request.Name, request.Sku, request.Description, request.Price);
        product = await products.AddAsync(product, cancellationToken);
        await publisher.PublishAsync(product, cancellationToken);

        var model = ProductInventoryReadModelMapper.Map(product);
        var result = new CreateProductResult(model);

        return result;
    }
}