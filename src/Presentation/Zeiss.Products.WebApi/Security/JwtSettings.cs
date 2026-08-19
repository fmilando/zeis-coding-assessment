namespace Zeiss.Products.WebApi.Security;

internal class JwtSettings
{
    public const string SectionName = "Jwt";
    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int TokenExpirationMinutes { get; set; }
    
    public const string SecretIdClaimName = "Jwt:SecretId";
    public const string SecretKeyClaimName = "Jwt:SecretKey";
}