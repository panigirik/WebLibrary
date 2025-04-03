using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces
{
    /// <summary>
    /// Интерфейс для валидации добавления книги.
    /// </summary>
    public interface IAddBookUseCaseValidation
    {
        /// <summary>
        /// Асинхронно выполняет валидацию и добавление книги.
        /// </summary>
        /// <param name="bookDto">Объект с данными книги.</param>
        /// <param name="file">Файл изображения книги.</param>
        /// <returns>Результат выполнения use-case (успех или ошибка).</returns>
        Task<ValidationResult> ExecuteAsync(AddBookRequest bookDto, IFormFile file);
    }
}