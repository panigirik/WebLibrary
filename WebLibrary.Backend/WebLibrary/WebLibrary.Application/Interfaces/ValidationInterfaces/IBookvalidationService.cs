using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

/// <summary>
/// Интерфейс для сервиса валидации книг.
/// </summary>
public interface IBookValidationService
{
    /// <summary>
    /// Асинхронно выполняет валидацию данных книги.
    /// </summary>
    /// <param name="bookDto">Объект с данными книги.</param>
    /// <param name="file">Файл изображения книги.</param>
    /// <returns>Результат валидации.</returns>
    Task<ValidationResult> ValidateBookAsync(AddBookRequest bookDto, IFormFile file);
}