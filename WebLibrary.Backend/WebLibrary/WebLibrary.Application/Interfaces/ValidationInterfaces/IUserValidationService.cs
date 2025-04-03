using FluentValidation.Results;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

/// <summary>
/// Интерфейс для сервиса валидации пользователей.
/// </summary>
public interface IUserValidationService
{
    /// <summary>
    /// Асинхронно выполняет валидацию данных книги.
    /// </summary>
    /// <param name="updateUserInfoRequest">Объект с данными книги.</param>
    /// <returns>Результат валидации.</returns>
    Task<ValidationResult> ValidateBookAsync(UpdateUserInfoRequest updateUserInfoRequest);
}