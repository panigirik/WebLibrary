using FluentValidation;
using FluentValidation.Results;
using WebLibrary.Application.Interfaces.UseCases;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.UseCases;

/// <summary>
/// Сервис для выполнения use-case обновления информации о пользователе с валидацией.
/// </summary>
public class UpdateUserValidationService : IUpdateUserValidationService
{
    private readonly IValidator<UpdateUserInfoRequest> _updateUserValidator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UpdateUserInfoUseCase"/>.
    /// </summary>
    /// <param name="updateUserValidator">Валидатор для запросов обновления данных пользователя.</param>
    public UpdateUserValidationService(IValidator<UpdateUserInfoRequest> updateUserValidator)
    {
        _updateUserValidator = updateUserValidator;
    }

    /// <summary>
    /// Выполняет валидацию и обновление данных пользователя.
    /// </summary>
    /// <param name="updateUserInfoRequest">Запрос с данными для обновления информации о пользователе.</param>
    /// <returns>Результат выполнения use-case (успех или ошибка).</returns>
    public async Task<ValidationResult> ExecuteAsync(UpdateUserInfoRequest updateUserInfoRequest)
    {
        var validationResult = await _updateUserValidator.ValidateAsync(updateUserInfoRequest);
            
        if (!validationResult.IsValid)
        {
            return validationResult;
        }
        
        return validationResult;
    }
}