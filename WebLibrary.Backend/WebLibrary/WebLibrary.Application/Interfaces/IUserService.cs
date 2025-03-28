using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task AddUserAsync(UserDto user);
    Task UpdateUserAsync(UserDto user);
    Task DeleteUserAsync(Guid id);
}