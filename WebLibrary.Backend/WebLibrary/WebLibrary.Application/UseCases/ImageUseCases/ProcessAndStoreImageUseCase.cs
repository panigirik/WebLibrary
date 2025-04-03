using Microsoft.AspNetCore.Http;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.ImageInterfaces;

namespace WebLibrary.Application.UseCases.ImageUseCases;

    /// <summary>
    /// Use Case для обработки и сохранения изображения.
    /// </summary>
    public class ProcessAndStoreImageUseCase : IProcessAndStoreImageUseCase
    {
        /// <summary>
        /// Обрабатывает и сохраняет изображение в виде массива байтов.
        /// </summary>
        /// <param name="imageFile">Файл изображения, переданный через форму.</param>
        /// <returns>Массив байтов, представляющий изображение, если файл был передан. Если файл пустой, возвращает <c>null</c>.</returns>
        public async Task<byte[]?> ExecuteAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
