using FluentValidation.Results;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

/// <summary>
/// Интерфейс для валидации запросов и пользователей.
/// </summary>
public interface ILoginValidationService
{
    /// <summary>
    /// Асинхронно выполняет валидацию запроса на вход в систему.
    /// </summary>
    /// <param name="request">Данные запроса для входа.</param>
    /// <returns>Результат валидации.</returns>
    Task<ValidationResult> ValidateLoginRequestAsync(LoginRequest request);

    /// <summary>
    /// Асинхронно выполняет валидацию данных пользователя.
    /// </summary>
    /// <param name="request">Данные пользователя.</param>
    /// <returns>Результат валидации.</returns>
    Task<ValidationResult> ValidateUserAsync(LoginRequest request);
}