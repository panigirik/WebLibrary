using Microsoft.AspNetCore.Http;

namespace WebLibrary.Application.Interfaces;

public interface IImageService
{
    Task<byte[]?> ProcessAndStoreImageAsync(IFormFile imageFile);
    Task<byte[]?> GetImageAsync(Guid bookId);
}