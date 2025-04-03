using FluentValidation;
using FluentValidation.Results;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.Services;

/// <summary>
/// Сервис для валидации пользователей.
/// </summary>
public class UserValidationService : IUserValidationService
{
    private readonly IValidator<UpdateUserInfoRequest> _updateUserValidator;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidationService"/>.
    /// </summary>
    /// <param name="updateUserValidator">Валидатор для запросов логина.</param>
    public UserValidationService(IValidator<UpdateUserInfoRequest> updateUserValidator)
    {
        _updateUserValidator = updateUserValidator;
    }
    
    /// <summary>
    /// Выполняет валидацию пользователя.
    /// </summary>
    /// <param name="request">Запрос на обновление полей пользователя.</param>
    /// <returns>Результат валидации.</returns>
    public async Task<ValidationResult> ValidateBookAsync(UpdateUserInfoRequest updateUserInfoRequest)
    {
        return await _updateUserValidator.ValidateAsync(updateUserInfoRequest);
    }
}