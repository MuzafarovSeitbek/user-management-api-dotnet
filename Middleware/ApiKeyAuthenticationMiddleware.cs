using System.Text.Json;
using UserManagementAPI.Models;

namespace UserManagementAPI.Middleware;

/// <summary>
/// Middleware for API key authentication
/// </summary>
public class ApiKeyAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiKeyAuthenticationMiddleware> _logger;
    private const string API_KEY_HEADER = "X-API-Key";

    public ApiKeyAuthenticationMiddleware(
        RequestDelegate next, 
        IConfiguration configuration,
        ILogger<ApiKeyAuthenticationMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authentication for root endpoint and Swagger
        var path = context.Request.Path.Value?.ToLower() ?? "";
        if (path == "/" || path.StartsWith("/swagger") || path.StartsWith("/api/health"))
        {
            await _next(context);
            return;
        }

        // Check if API key header exists
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            _logger.LogWarning("API key missing for request: {Path}", context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            
            var errorResponse = new ErrorResponse
            {
                Error = "Unauthorized",
                Message = $"API key is required. Please provide {API_KEY_HEADER} header."
            };
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            return;
        }

        // Validate API key
        var apiKey = _configuration["ApiSettings:ApiKey"];
        if (extractedApiKey != apiKey)
        {
            _logger.LogWarning("Invalid API key attempt for request: {Path}", context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            
            var errorResponse = new ErrorResponse
            {
                Error = "Forbidden",
                Message = "Invalid API key."
            };
            
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            return;
        }

        // API key is valid, proceed to next middleware
        await _next(context);
    }
}


