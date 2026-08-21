using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Moq;
using Zeiss.Products.Domain.Entities;

namespace Zeiss.Products.IntegrationTests.WebApi.Endpoints.Products;

public class UpdateProductTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UpdateProductTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        _factory.ProductRepositoryMock.Invocations.Clear();
    }

    [Fact]
    public async Task HandleAsync_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var request = new
        {
            Name = "Updated",
            Sku = "SKU",
            Description = "Desc",
            Price = 150m,
            IsActive = true
        };

        _factory.ProductRepositoryMock.Setup(x =>
            x.GetAsync(99, It.IsAny<CancellationToken>())
        ).ReturnsAsync((Product?)null);

        // Act
        var response = await _client.PutAsJsonAsync("/api/products/99", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
