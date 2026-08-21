using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Moq;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.IntegrationTests.WebApi.Endpoints.Products;

public class CreateProductTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CreateProductTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        _factory.ProductRepositoryMock.Invocations.Clear();
    }

    [Fact]
    public async Task HandleAsync_ReturnsCreated_WhenHappyPath()
    {
        // Arrange
        var request = new
        {
            Name = "New Product",
            Sku = "SKU-NEW",
            Description = "Desc",
            Price = 100m,
            IsActive = true
        };

        var product = new Product(
            1,
            "New Product",
            "SKU-NEW",
            "Desc",
            100m,
            true,
            false,
            DateTime.UtcNow,
            null,
            null, 
            null);

        _factory.ProductRepositoryMock.Setup(x =>
            x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(product);

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenNonHappyPath()
    {
        // Arrange
        var request = new
        {
            Name = "",
            Sku = "SKU",
            Description = "Desc",
            Price = -10m,
            IsActive = true
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/products", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
