using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Zeiss.Products.Application.Interfaces.Messaging;
using Zeiss.Products.Application.Interfaces.Repositories;

namespace Zeiss.Products.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public Mock<IProductRepository> ProductRepositoryMock { get; } = new();
    public Mock<IInventoryRepository> InventoryRepositoryMock { get; } = new();
    public Mock<IEventPublisher> EventPublisherMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IProductRepository>();
            services.RemoveAll<IInventoryRepository>();
            services.RemoveAll<IEventPublisher>();

            services.AddScoped(_ => ProductRepositoryMock.Object);
            services.AddScoped(_ => InventoryRepositoryMock.Object);
            services.AddScoped(_ => EventPublisherMock.Object);

            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, Auth.TestAuthHandler>("Test", options => { });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder("Test")
                    .RequireAuthenticatedUser()
                    .Build();
            });
        });
    }
}
