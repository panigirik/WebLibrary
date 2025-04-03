using FluentValidation;
using FluentValidation.Results;
using WebLibrary.Application.Interfaces.UseCases;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.Services;

/// <summary>
/// Сервис для выполнения use-case обновления информации о пользователе с валидацией.
/// </summary>
public class UpdateUserInfoUseCase : IUpdateUserInfoUseCase
{
    private readonly IValidator<UpdateUserInfoRequest> _updateUserValidator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UpdateUserInfoUseCase"/>.
    /// </summary>
    /// <param name="updateUserValidator">Валидатор для запросов обновления данных пользователя.</param>
    public UpdateUserInfoUseCase(IValidator<UpdateUserInfoRequest> updateUserValidator)
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
        // Выполняем валидацию данных пользователя
        var validationResult = await _updateUserValidator.ValidateAsync(updateUserInfoRequest);
            
        if (!validationResult.IsValid)
        {
            return validationResult;
        }

        // Дополнительная бизнес-логика для обновления пользователя может быть добавлена здесь
            
        return validationResult;
    }
}