namespace Zeiss.Products.WebApi.Security;

internal class JwtSettings
{
    public const string SectionName = "Jwt";
    public const string UserUniqueIdClaimName = "Jwt:UserUniqueId";

    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int TokenExpirationMinutes { get; set; }
}