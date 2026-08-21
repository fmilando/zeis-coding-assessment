using System.Data;
using MediatR;
using Zeiss.Products.Application.Features.Products.Queries;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;
using Zeiss.Products.Application.Results;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.Application.Features.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IUnitOfWork unitOfWork,
    IProductRepository products,
    IInventoryRepository inventories,
    IEventPublisher publisher
) : IRequestHandler<CreateProductCommand, Result<ProductInventoryReadModel>>
{
    public async Task<Result<ProductInventoryReadModel>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        //Note: PostgreSQL prevents phantom records with the Repeatable Read level
        await unitOfWork.StartAsync(IsolationLevel.RepeatableRead, cancellationToken);

        var product = await products.GetBySkuAsync(request.Sku, cancellationToken);

        if (product is not null)
        {
            return new Error(
                ErrorCodes.Product.SkuConflict,
                $"Product {request.Sku} already assigned to a product"
            );
        }

        product = new Product(request.Name, request.Sku, request.Description, request.Price);
        product = await products.AddAsync(product, cancellationToken);

        if (request.Quantity >= 0)
        {
            var inventory = new Inventory(product.Id, request.Quantity!.Value);
            await inventories.AddAsync(inventory, cancellationToken);
            product.SetInventory(inventory);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        await publisher.PublishAsync(product, cancellationToken);

        return product.Inventory switch
        {
            null => ProductInventoryReadModelMapper.Map(product),
            _ => ProductInventoryReadModelMapper.Map(product, product.Inventory)
        };
    }
}