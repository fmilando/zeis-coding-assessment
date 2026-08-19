using System.Net;
using System.Net.Http.Headers;

namespace Zeiss.Products.IntegrationTests.WebApi.Endpoints.Products;

public class GetByStockLevelTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public GetByStockLevelTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        _factory.ProductRepositoryMock.Invocations.Clear();
    }

    [Fact]
    public async Task HandleAsync_ReturnsBadRequest_WhenNonHappyPath()
    {
        // Act (negative threshold might trigger validation error)
        var response = await _client.GetAsync("/api/products/stock-level?threshold=-1");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
