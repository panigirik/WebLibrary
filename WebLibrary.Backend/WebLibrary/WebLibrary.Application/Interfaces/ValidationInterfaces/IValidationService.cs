using FluentValidation.Results;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

/// <summary>
/// Интерфейс для сервисов валидации пользователей и запросов на вход.
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// Асинхронно выполняет валидацию запроса на вход в систему.
    /// </summary>
    /// <param name="request">Данные запроса для входа.</param>
    /// <returns>Результат валидации.</returns>
    Task<ValidationResult> ValidateLoginRequestAsync(LoginRequest request);

    /// <summary>
    /// Асинхронно выполняет валидацию пользователя.
    /// </summary>
    /// <param name="request">Данные пользователя.</param>
    /// <returns>Результат валидации.</returns>
    Task<ValidationResult> ValidateUserAsync(LoginRequest request);
}