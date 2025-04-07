namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.ImageInterfaces;

/// <summary>
/// Интерфейс для получения изображения книги.
/// </summary>
public interface IGetImageService
{
    Task<byte[]?> ExecuteAsync(Guid bookId);
}