using Serilog;
using Zeiss.Products.Application;
using Zeiss.Products.Infrastructure;
using Zeiss.Products.Infrastructure.Logging;
using Zeiss.Products.WebApi.Converters;
using Zeiss.Products.WebApi.Endpoints;
using Zeiss.Products.WebApi.Endpoints.HealthChecks;
using Zeiss.Products.WebApi.Middlewares;
using Zeiss.Products.WebApi.Security;
using Zeiss.Products.WebApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDefaultJsonDateTimeConverter();
builder.Services.AddApiSecurity(builder.Configuration);
builder.Services.AddSwaggerPage(builder.Configuration);
builder.Host.AddApiLogging(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseSwaggerPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseHealthCheckEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints();

try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unexpectedly the application faulted");
}
finally
{
    Log.CloseAndFlush();
}
