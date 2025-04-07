using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

/// <summary>
/// Интерфейс для валидации добавлнтя пользователя.
/// </summary>
public interface IAddUserValidationService
{
    /// <summary>
    /// Асинхронно выполняет валидацию запроса на создание пользователя.
    /// </summary>
    /// <param name="userDto">Данные запроса для входа.</param>
    /// <returns>Результат валидации.</returns>
    Task ValidateAsync(UserDto userDto);
}