using Microsoft.AspNetCore.Http;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.ServiceInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

/// <summary>
/// Сервис для работы с изображениями книг (получение и сохранение).
/// </summary>
public class ImageService : IImageService
{
    private readonly IBookRepository _bookRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ImageService"/>.
    /// </summary>
    /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
    public ImageService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    /// <summary>
    /// Получает изображение книги по идентификатору.
    /// </summary>
    /// <param name="bookId">Идентификатор книги.</param>
    /// <returns>Изображение книги в виде массива байтов, если оно существует, иначе <c>null</c>.</returns>
    public async Task<byte[]?> GetImageAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        return book?.ImageData;
    }

    /// <summary>
    /// Обрабатывает и сохраняет изображение в виде массива байтов.
    /// </summary>
    /// <param name="imageFile">Файл изображения, переданный через форму.</param>
    /// <returns>Массив байтов, представляющий изображение, если файл был передан. Если файл пустой, возвращает <c>null</c>.</returns>
    public async Task<byte[]?> ProcessImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            throw new NotFoundException("image not found");
        }
        using var memoryStream = new MemoryStream();
        await imageFile.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}