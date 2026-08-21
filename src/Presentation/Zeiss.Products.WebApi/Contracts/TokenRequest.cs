namespace Zeiss.Products.WebApi.Contracts;

internal sealed class TokenRequest
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}