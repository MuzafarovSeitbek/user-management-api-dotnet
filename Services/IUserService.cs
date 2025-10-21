using UserManagementAPI.Models;
using UserManagementAPI.Models.DTOs;

namespace UserManagementAPI.Services;

/// <summary>
/// Interface for user management operations
/// </summary>
public interface IUserService
{
    List<User> GetAllUsers();
    User? GetUserById(Guid id);
    User CreateUser(CreateUserDto userDto);
    User? UpdateUser(Guid id, UpdateUserDto userDto);
    User? DeleteUser(Guid id);
    bool UsernameExists(string username, Guid? excludeUserId = null);
    bool EmailExists(string email, Guid? excludeUserId = null);
}


