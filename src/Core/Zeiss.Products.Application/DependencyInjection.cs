using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Zeiss.Products.Application.Features.Inventories.Commands.AddToStock;
using Zeiss.Products.Application.Features.Inventories.Commands.DecrementStock;
using Zeiss.Products.Application.Features.Products.Commands.CreateProduct;
using Zeiss.Products.Application.Features.Products.Commands.DeleteProduct;
using Zeiss.Products.Application.Features.Products.Commands.UpdateProduct;
using Zeiss.Products.Application.Features.Products.Queries.GetByStockLevel;
using Zeiss.Products.Application.Features.Products.Queries.GetProductById;
using Zeiss.Products.Application.Features.Products.Queries.GetProducts;
using Zeiss.Products.Application.Features.Products.Queries.SearchProducts;
using Zeiss.Products.Application.Interfaces.Handlers;

namespace Zeiss.Products.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<IRequestDispatcher, RequestDispatcher>()
            .AddCommandHandlers()
            .AddQueryHandlers();
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddRequestHandler<AddToStockCommand, AddToStockResult, AddToStockCommandHandler, AddToStockCommandValidator>()
            .AddRequestHandler<CreateProductCommand, CreateProductResult, CreateProductCommandHandler, CreateProductCommandValidator>()
            .AddRequestHandler<DecrementStockCommand, DecrementStockResult, DecrementStockCommandHandler, DecrementStockCommandValidator>()
            .AddRequestHandler<DeleteProductCommand, DeleteProductResult, DeleteProductCommandHandler, DeleteProductCommandValidator>()
            .AddRequestHandler<UpdateProductCommand, UpdateProductResult, UpdateProductCommandHandler, UpdateProductCommandValidator>();
    }

    private static void AddQueryHandlers(this IServiceCollection services)
    {
        services
            .AddRequestHandler<GetByStockLevelQuery, GetByStockLevelResult, GetByStockLevelQueryHandler, GetByStockLevelQueryValidator>()
            .AddRequestHandler<GetProductByIdQuery, GetProductByIdResult, GetProductByIdQueryHandler, GetProductByIdQueryValidator>()
            .AddRequestHandler<GetProductsQuery, GetProductsResult, GetProductsQueryHandler, GetProductsQueryValidator>()
            .AddRequestHandler<SearchProductsQuery, SearchProductsResult, SearchProductsQueryHandler, SearchProductsQueryValidator>();
    }

    private static IServiceCollection AddRequestHandler<TRequest, TResponse, THandler, TValidator>(
        this IServiceCollection services
    )
    where TRequest : class
    where TResponse : class
    where THandler : class, IRequestHandler<TRequest, TResponse>
    where TValidator : class, IValidator<TRequest>
    {
        services.AddScoped<IRequestHandler<TRequest, TResponse>, THandler>();
        services.AddScoped<IValidator<TRequest>, TValidator>();
        return services;
    }
}
