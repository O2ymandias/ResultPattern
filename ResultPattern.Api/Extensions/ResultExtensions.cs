using Microsoft.AspNetCore.Mvc;
using ResultPattern.Api.Responses;
using ResultPattern.Core.Errors;
using ResultPattern.Core.Results;

namespace ResultPattern.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            var successResponse = ApiResponse<T>.SuccessResponse(result.Value!);
            return new OkObjectResult(successResponse);
        }

        var errorResponse = ApiResponse<T>.ErrorResponse(result.Error!);
        return result.Error!.ErrorType switch
        {
            ErrorType.Validation => new BadRequestObjectResult(errorResponse),
            ErrorType.NotFound => new NotFoundObjectResult(errorResponse),
            ErrorType.Conflict => new ConflictObjectResult(errorResponse),
            _ => new ObjectResult(errorResponse)
                { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }

    public static IActionResult ToActionResult(this Result result)
    {
        var successResponse = ApiResponse<object>.SuccessResponse(new
        {
            Message = "Operation completed successfully"
        });
        if (result.IsSuccess) return new OkObjectResult(successResponse);

        var errorResponse = ApiResponse<object>.ErrorResponse(result.Error!);
        return result.Error!.ErrorType switch
        {
            ErrorType.Validation => new BadRequestObjectResult(errorResponse),
            ErrorType.NotFound => new NotFoundObjectResult(errorResponse),
            ErrorType.Conflict => new ConflictObjectResult(errorResponse),
            _ => new ObjectResult(errorResponse) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }

    public static IActionResult ToPagedResult<T>(this Result<PagedResult<T>> result)
    {
        if (result.IsSuccess)
        {
            var pagedResult = result.Value!;
            return new OkObjectResult(PagedApiResponse<T>.SuccessResponse(pagedResult.Items, pagedResult.Pagination));
        }

        return result.Error!.ErrorType switch
        {
            ErrorType.Validation => new BadRequestObjectResult(PagedApiResponse<T>.ErrorResponse(result.Error)),
            _ => new ObjectResult(PagedApiResponse<T>.ErrorResponse(result.Error))
                { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }
}