using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Api.DJ;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();

        if (exception is FluentValidation.ValidationException fluentException)
        {
            problemDetails.Title = "One or more validation errors occurred.";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var validationErrors = fluentException.Errors.Select(error => error.ErrorMessage).ToList();
            problemDetails.Extensions.Add("errors", validationErrors);
        }
        else
        {
            problemDetails.Title = "Internal Server Error";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}
