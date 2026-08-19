using Zeiss.Products.WebApi.Helpers;
using ILogger = Serilog.ILogger;
namespace Zeiss.Products.WebApi.Middlewares;

internal class GlobalExceptionMiddleware(
    RequestDelegate next, 
    ILogger logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var endpoint = HttpContextHelper.GetRequestEndpoint(context);
            logger.Error(ex, "Error processing request to '{Endpoint}'", endpoint);
            throw;
        }
    }
}