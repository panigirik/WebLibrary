using Microsoft.AspNetCore.Http;

namespace WebLibrary.Application.Interfaces.ServiceInterfaces;

/// <summary>
/// Интерфейс для работы с изображениями книг (получение и сохранение).
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Получает изображение книги по идентификатору.
    /// </summary>
    Task<byte[]?> GetImageAsync(Guid bookId);

    /// <summary>
    /// Обрабатывает и сохраняет изображение в виде массива байтов.
    /// </summary>
    Task<byte[]?> ProcessImageAsync(IFormFile imageFile);
}
