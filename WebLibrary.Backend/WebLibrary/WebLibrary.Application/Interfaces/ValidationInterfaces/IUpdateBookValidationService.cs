using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

    /// <summary>
    /// Интерфейс для валидации обновления книги.
    /// </summary>
    public interface IUpdateBookValidationService
    {
        /// <summary>
        /// Метод для валидации данных для обновления книги.
        /// </summary>
        Task ValidateAsync(UpdateBookRequest updateBookRequest);
    }
