using UserManagementAPI.Models;
using UserManagementAPI.Models.DTOs;

namespace UserManagementAPI.Services;

/// <summary>
/// In-memory user service implementation
/// </summary>
public class UserService : IUserService
{
    private readonly List<User> _users;

    public UserService()
    {
        // Initialize with sample data
        _users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Username = "johndoe",
                Email = "john@example.com",
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "janedoe",
                Email = "jane@example.com",
                FirstName = "Jane",
                LastName = "Doe",
                Age = 28,
                CreatedAt = DateTime.UtcNow
            }
        };
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }

    public User? GetUserById(Guid id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(CreateUserDto userDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = userDto.Username,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Age = userDto.Age,
            CreatedAt = DateTime.UtcNow
        };

        _users.Add(user);
        return user;
    }

    public User? UpdateUser(Guid id, UpdateUserDto userDto)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return null;

        if (!string.IsNullOrWhiteSpace(userDto.Username))
            user.Username = userDto.Username;

        if (!string.IsNullOrWhiteSpace(userDto.Email))
            user.Email = userDto.Email;

        if (!string.IsNullOrWhiteSpace(userDto.FirstName))
            user.FirstName = userDto.FirstName;

        if (!string.IsNullOrWhiteSpace(userDto.LastName))
            user.LastName = userDto.LastName;

        if (userDto.Age.HasValue)
            user.Age = userDto.Age;

        user.UpdatedAt = DateTime.UtcNow;

        return user;
    }

    public User? DeleteUser(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
        }
        return user;
    }

    public bool UsernameExists(string username, Guid? excludeUserId = null)
    {
        return _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) 
                               && (!excludeUserId.HasValue || u.Id != excludeUserId.Value));
    }

    public bool EmailExists(string email, Guid? excludeUserId = null)
    {
        return _users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) 
                               && (!excludeUserId.HasValue || u.Id != excludeUserId.Value));
    }
}


