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
            result.Errors?.Select(x => new ApiError(x.Code, x.Message)).ToArray(),
            new
            {
                Timestamp = DateTime.UtcNow,
            }
        );
    }
}