using System.Net;
using System.Net.Http.Headers;
using Moq;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.IntegrationTests.WebApi.Endpoints.Products;

public class DeleteProductTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DeleteProductTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        _factory.ProductRepositoryMock.Invocations.Clear();
    }

    [Fact]
    public async Task HandleAsync_ReturnsNoContent_WhenHappyPath()
    {
        // Arrange
        var product = new Product(
            100_001,
            "Test",
            "SKU-1",
            null,
            100m,
            true,
            false,
            DateTime.UtcNow,
            null,
            null
        );
        
        _factory.ProductRepositoryMock.Setup(x => 
            x.GetByIdAsync(100_001, It.IsAny<CancellationToken>())
        ).ReturnsAsync(product);

        // Act
        var response = await _client.DeleteAsync("/api/products/100001");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
