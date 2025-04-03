using Microsoft.AspNetCore.Http;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

public class ImageService : IImageService
{
    private readonly IBookRepository _bookRepository;

    public ImageService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<byte[]?> ProcessAndStoreImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return null;

        using var memoryStream = new MemoryStream();
        await imageFile.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<byte[]?> GetImageAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        return book?.ImageData;
    }
}
