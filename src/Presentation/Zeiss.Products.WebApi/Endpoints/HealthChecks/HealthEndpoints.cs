using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Zeiss.Products.WebApi.Endpoints.HealthChecks;

internal static class HealthEndpoints
{
    public static void UseHealthCheckEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapHealthChecks("/health", new HealthCheckOptions()
        {
            Predicate = _ => false
        }).AllowAnonymous();

        routes.MapHealthChecks("/health/live", new HealthCheckOptions()
        {
            Predicate = _ => false
        }).AllowAnonymous();

        routes.MapHealthChecks("/health/ready", new HealthCheckOptions()
        {
            Predicate = check => check.Tags.Contains("ready")
        }).AllowAnonymous();
    }
}