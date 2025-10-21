using System.Diagnostics;

namespace UserManagementAPI.Middleware;

/// <summary>
/// Middleware for logging HTTP requests and responses
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var timestamp = DateTime.UtcNow;
        var method = context.Request.Method;
        var path = context.Request.Path;
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        _logger.LogInformation("[{Timestamp}] {Method} {Path} - IP: {IP}", 
            timestamp, method, path, ip);

        // Start stopwatch for response time
        var stopwatch = Stopwatch.StartNew();

        // Call the next middleware in the pipeline
        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation("[{Timestamp}] {Method} {Path} - Status: {StatusCode} - {Duration}ms",
            timestamp, method, path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
    }
}


