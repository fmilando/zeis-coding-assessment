namespace Zeiss.Products.WebApi.Contracts;

internal sealed class TokenRequest
{
    public required string SecretId { get; set; }
    public required string SecretKey { get; set; }
}