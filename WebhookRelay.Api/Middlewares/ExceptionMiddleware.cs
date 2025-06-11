using System.Net;
using System.Text.Json;
using WebhookRelay.Application.Common.Exceptions;

namespace WebhookRelay.Web.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
    private readonly IHostEnvironment _env = env;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BadHttpRequestException ex)
        {
            await HandleSpecificExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid request.", LogLevel.Warning);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleSpecificExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Unauthorized access.", LogLevel.Warning);
        }
        catch (NotFoundException ex)
        {
            await HandleSpecificExceptionAsync(context, ex, HttpStatusCode.NotFound, ex.Message, LogLevel.Warning);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleGenericExceptionAsync(context, ex);
        }
    }

    private async Task HandleSpecificExceptionAsync(
        HttpContext context,
        Exception ex,
        HttpStatusCode statusCode,
        string message,
        LogLevel logLevel)
    {
        var traceId = context.TraceIdentifier;
        _logger.Log(logLevel, ex, "Handled {ExceptionType} for {Method} {Path} (TraceId: {TraceId})",
            ex.GetType().Name, context.Request.Method, context.Request.Path, traceId);

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            TraceId = traceId,
            Message = message,
            Details = _env.IsDevelopment() ? ex.Message : null
        };

        await WriteResponseAsync(context, response);
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        var traceId = context.TraceIdentifier;
        _logger.LogWarning(ex, "Validation error on {Method} {Path} (TraceId: {TraceId})",
            context.Request.Method, context.Request.Path, traceId);

        var response = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.UnprocessableEntity,
            TraceId = traceId,
            Message = "Validation failed.",
            Errors = ex.Errors,
            Details = _env.IsDevelopment() ? ex.Message : null
        };

        await WriteResponseAsync(context, response);
    }

    private async Task HandleGenericExceptionAsync(HttpContext context, Exception ex)
    {
        var traceId = context.TraceIdentifier;
        _logger.LogError(ex, "Unhandled exception for {Method} {Path} (TraceId: {TraceId})",
            context.Request.Method, context.Request.Path, traceId);

        var response = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            TraceId = traceId,
            Message = "An unexpected error occurred.",
            Details = _env.IsDevelopment() ? ex.ToString() : null
        };

        await WriteResponseAsync(context, response);
    }

    private static async Task WriteResponseAsync(HttpContext context, ErrorResponse errorResponse)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        await context.Response.WriteAsync(json);
    }
}
