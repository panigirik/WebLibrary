using WebLibrary.Application.Dtos;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces;

/// <summary>
/// Интерфейс для сервиса управления пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получает всех пользователей.
    /// </summary>
    Task<IEnumerable<UserDto>> GetAllUsersAsync();

    /// <summary>
    /// Получает пользователя по его идентификатору.
    /// </summary>
    Task<UserDto?> GetUserByIdAsync(Guid id);

    /// <summary>
    /// Получает пользователя по его email.
    /// </summary>
    Task<UserDto?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Добавляет нового пользователя.
    /// </summary>
    Task AddUserAsync(UserDto user);

    /// <summary>
    /// Обновляет данные пользователя.
    /// </summary>
    Task UpdateUserAsync(UpdateUserInfoRequest updateUserInfoRequest);

    /// <summary>
    /// Удаляет пользователя по его идентификатору.
    /// </summary>
    Task DeleteUserAsync(Guid id);
}