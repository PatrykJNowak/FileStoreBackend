using FileStore.Api.DJ.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.DJ;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case FluentValidation.ValidationException fluentException:
            {
                problemDetails.Title = "Validation error";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var validationErrors = fluentException.Errors.Select(error => error.ErrorMessage).First();
                problemDetails.Extensions.Add("errorMessage", validationErrors);
                break;
            }
            case UnauthorizeException _:
                problemDetails.Title = "Unauthorized";
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                break;
            default:
                problemDetails.Title = "Internal Server Error";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}