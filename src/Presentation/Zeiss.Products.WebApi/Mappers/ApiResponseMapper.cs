using Zeiss.Products.Application.Results;
using Zeiss.Products.WebApi.Contracts;

namespace Zeiss.Products.WebApi.Mappers;

internal static class ApiResponseMapper
{
    public static object ToApiResponse<T>(this Result<T> result)
    {
        return new ApiResponse<T>(
            result.IsSuccess,
            result.Value,
            result.Errors.Select(x => new ApiMessage(x.Code, x.Message)).ToArray(),
            new
            {
                Timestamp = DateTime.UtcNow,
            }
        );
    }
    
    public static object ToApiResponse<T>(this T data, bool success = true)
    {
        return new ApiResponse<T>(
            data is not null && success,
            data,
            [],
            new
            {
                Timestamp = DateTime.UtcNow,
            }
        );
    }
}