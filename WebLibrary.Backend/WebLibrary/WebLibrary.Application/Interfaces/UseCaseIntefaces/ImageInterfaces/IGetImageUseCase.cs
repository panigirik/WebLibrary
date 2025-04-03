namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.ImageInterfaces;

/// <summary>
/// Интерфейс для получения изображения книги.
/// </summary>
public interface IGetImageUseCase
{
    Task<byte[]?> ExecuteAsync(Guid bookId);
}