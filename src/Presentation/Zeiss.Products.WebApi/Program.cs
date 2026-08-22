using Serilog;
using Zeiss.Products.Application;
using Zeiss.Products.Infrastructure;
using Zeiss.Products.Infrastructure.Logging;
using Zeiss.Products.WebApi.ApiReference;
using Zeiss.Products.WebApi.Configurations;
using Zeiss.Products.WebApi.Converters;
using Zeiss.Products.WebApi.Endpoints;
using Zeiss.Products.WebApi.Endpoints.HealthChecks;
using Zeiss.Products.WebApi.Exceptions;
using Zeiss.Products.WebApi.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDefaultJsonDateTimeConverter();
builder.Services.AddApiSecurity(builder.Configuration);
builder.Services.AddApiReference(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddJsonSerializationOptions();
builder.Host.AddApiLogging(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseApiReference();

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
