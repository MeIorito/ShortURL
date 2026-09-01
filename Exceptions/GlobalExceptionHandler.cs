using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ShortURL.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "An unhandled exception occurred.");

        var statusCode = exception switch
        {
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            InvalidCredentialsException => StatusCodes.Status401Unauthorized,
            EmailAlreadyExistsException => StatusCodes.Status409Conflict,
            UserNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var title = exception switch
        {
            FluentValidation.ValidationException => "Validation failed",
            InvalidCredentialsException => "Invalid credentials",
            EmailAlreadyExistsException => "Email already exists",
            UserNotFoundException => "User not found",
            _ => "Internal server error"
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception switch
            {
                FluentValidation.ValidationException => "One or more validation errors occurred.",
                _ => exception.Message
            },
            Instance = httpContext.Request.Path
        };

        if (exception is FluentValidation.ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            problemDetails.Extensions["errors"] = errors;
        }

        problemDetails.Extensions["traceId"] =
            httpContext.TraceIdentifier;

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}