using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Api.Extensions;

public static class ControllerBaseExtensions
{
    private const string ErrorCodeFieldName = "errorCode";
    public static ActionResult ToActionResult<T>(this ControllerBase controller, ErrorOr<T> result, string errorMessage)
    {
        return result switch
        {
            { IsError: true } => controller.ToErrorWithProblemDetails(errorMessage, result.FirstError),
            { Value: null } => controller.NotFound(),
            { Value: Created } => controller.Created(),
            { Value: Updated or Deleted } => controller.NoContent(),
            { Value: _ } => controller.Ok(result.Value)
        };
    }

    private static ObjectResult ToErrorWithProblemDetails(this ControllerBase controller, string message, Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Failure => StatusCodes.Status422UnprocessableEntity,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = controller.ProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            message,
            detail: error.Description
        );

        problemDetails.Extensions.Add(ErrorCodeFieldName, error.Code);

        foreach (var (key, value) in error.Metadata ?? [])
        {
            problemDetails.Extensions.Add(key, value);
        }

        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }
}
