using FluentValidation.Results;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.UseCases
{
    /// <summary>
    /// Интерфейс валидации обновления информации о пользователе.
    /// </summary>
    public interface IUpdateUserValidationService
    {
        /// <summary>
        /// Асинхронно выполняет валидацию и обновление информации о пользователе.
        /// </summary>
        /// <param name="updateUserInfoRequest">Запрос с данными для обновления информации о пользователе.</param>
        /// <returns>Результат выполнения use-case (успех или ошибка).</returns>
        Task<ValidationResult> ExecuteAsync(UpdateUserInfoRequest updateUserInfoRequest);
    }
}