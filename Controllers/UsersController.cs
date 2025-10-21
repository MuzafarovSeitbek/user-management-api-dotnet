using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Models.DTOs;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

/// <summary>
/// Controller for managing users
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>List of all users</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<User>>), StatusCodes.Status200OK)]
    public ActionResult<ApiResponse<List<User>>> GetAllUsers()
    {
        try
        {
            var users = _userService.GetAllUsers();
            return Ok(ApiResponse<List<User>>.SuccessResponse(
                users, 
                count: users.Count
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users");
            return StatusCode(500, new ErrorResponse
            {
                Error = "Server Error",
                Message = "Failed to retrieve users"
            });
        }
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public ActionResult<ApiResponse<User>> GetUserById(Guid id)
    {
        try
        {
            var user = _userService.GetUserById(id);
            
            if (user == null)
            {
                return NotFound(new ErrorResponse
                {
                    Error = "Not Found",
                    Message = $"User with ID {id} not found"
                });
            }

            return Ok(ApiResponse<User>.SuccessResponse(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user {UserId}", id);
            return StatusCode(500, new ErrorResponse
            {
                Error = "Server Error",
                Message = "Failed to retrieve user"
            });
        }
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="createUserDto">User creation data</param>
    /// <returns>Created user</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public ActionResult<ApiResponse<User>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            // Check for validation errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ErrorResponse
                {
                    Error = "Validation Error",
                    Message = "Invalid user data",
                    Details = errors
                });
            }

            // Check if username already exists
            if (_userService.UsernameExists(createUserDto.Username))
            {
                return Conflict(new ErrorResponse
                {
                    Error = "Conflict",
                    Message = "Username already exists"
                });
            }

            // Check if email already exists
            if (_userService.EmailExists(createUserDto.Email))
            {
                return Conflict(new ErrorResponse
                {
                    Error = "Conflict",
                    Message = "Email already exists"
                });
            }

            var user = _userService.CreateUser(createUserDto);
            
            return CreatedAtAction(
                nameof(GetUserById), 
                new { id = user.Id }, 
                ApiResponse<User>.SuccessResponse(user, "User created successfully")
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500, new ErrorResponse
            {
                Error = "Server Error",
                Message = "Failed to create user"
            });
        }
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="updateUserDto">User update data</param>
    /// <returns>Updated user</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public ActionResult<ApiResponse<User>> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        try
        {
            // Check for validation errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ErrorResponse
                {
                    Error = "Validation Error",
                    Message = "Invalid user data",
                    Details = errors
                });
            }

            // Check if user exists
            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound(new ErrorResponse
                {
                    Error = "Not Found",
                    Message = $"User with ID {id} not found"
                });
            }

            // Check if new username conflicts with another user
            if (!string.IsNullOrWhiteSpace(updateUserDto.Username) && 
                _userService.UsernameExists(updateUserDto.Username, id))
            {
                return Conflict(new ErrorResponse
                {
                    Error = "Conflict",
                    Message = "Username already exists"
                });
            }

            // Check if new email conflicts with another user
            if (!string.IsNullOrWhiteSpace(updateUserDto.Email) && 
                _userService.EmailExists(updateUserDto.Email, id))
            {
                return Conflict(new ErrorResponse
                {
                    Error = "Conflict",
                    Message = "Email already exists"
                });
            }

            var updatedUser = _userService.UpdateUser(id, updateUserDto);
            
            return Ok(ApiResponse<User>.SuccessResponse(updatedUser!, "User updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {UserId}", id);
            return StatusCode(500, new ErrorResponse
            {
                Error = "Server Error",
                Message = "Failed to update user"
            });
        }
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>Deleted user</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public ActionResult<ApiResponse<User>> DeleteUser(Guid id)
    {
        try
        {
            var deletedUser = _userService.DeleteUser(id);
            
            if (deletedUser == null)
            {
                return NotFound(new ErrorResponse
                {
                    Error = "Not Found",
                    Message = $"User with ID {id} not found"
                });
            }

            return Ok(ApiResponse<User>.SuccessResponse(deletedUser, "User deleted successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {UserId}", id);
            return StatusCode(500, new ErrorResponse
            {
                Error = "Server Error",
                Message = "Failed to delete user"
            });
        }
    }
}


