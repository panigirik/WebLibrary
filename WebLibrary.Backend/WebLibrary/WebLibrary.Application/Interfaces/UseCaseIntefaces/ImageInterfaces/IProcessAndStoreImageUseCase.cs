using Microsoft.AspNetCore.Http;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.ImageInterfaces;

/// <summary>
/// Интерфейс для обработки и сохранения изображения.
/// </summary>
public interface IProcessAndStoreImageService
{
    Task<byte[]?> ExecuteAsync(IFormFile imageFile);
}