using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace Zeiss.Products.WebApi.ApiReference;

internal static class ApiReferenceExtensions
{
    private const string PageTitle = "Zeiss.Products.WebApi";

    public static void AddApiReference(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, ct) =>
            {
                ct.ThrowIfCancellationRequested();

                document.Info = new OpenApiInfo
                {
                    Title = PageTitle,
                    Description = "Vemba's solution of the .NET development hands-on assessment",
                    Version = "v1"
                };

                const string schemeName = "Bearer";
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>();
                document.Components.SecuritySchemes[schemeName] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Enter JWT access token obtained api/auth",
                };

                var securityScheme = new OpenApiSecuritySchemeReference(schemeName, document);
                document.Security ??= new List<OpenApiSecurityRequirement>();
                document.Security.Add(new OpenApiSecurityRequirement
                {
                    [securityScheme] = []
                });

                return Task.CompletedTask;
            });

            options.AddOperationTransformer((operation, context, ct) =>
            {
                ct.ThrowIfCancellationRequested();

                var isNotProtected = context.Description.ActionDescriptor.EndpointMetadata
                    .OfType<AllowAnonymousAttribute>()
                    .Any();

                if (isNotProtected)
                {
                    operation.Security = [];
                }

                return Task.CompletedTask;
            });
        });
    }

    public static void UseApiReference(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() is false)
        {
            return;
        }

        app.MapOpenApi();
        app.MapScalarApiReference("/api/docs", options =>
        {
            options.DarkMode = true;
            options.Layout = ScalarLayout.Modern;
        });
    }
}