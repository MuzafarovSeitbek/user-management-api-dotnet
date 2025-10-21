using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPI.Controllers;

/// <summary>
/// Health check and API information controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Get API health status and information
    /// </summary>
    [HttpGet]
    public ActionResult<object> GetHealth()
    {
        return Ok(new
        {
            message = "Welcome to User Management API",
            version = "1.0.0",
            status = "healthy",
            timestamp = DateTime.UtcNow,
            endpoints = new
            {
                users = "/api/users",
                swagger = "/swagger",
                health = "/api/health"
            }
        });
    }
}


