using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.ServiceInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для получения изображения книги.
    /// </summary>
    public class GetBookImageUseCase : IGetBookImageUseCase
    {
        private readonly IImageService _imageService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetBookImageUseCase"/>.
        /// </summary>
        /// <param name="getImageUseCase">Сервис для получения изображения книги.</param>
        public GetBookImageUseCase(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Получает изображение книги по идентификатору книги.
        /// </summary>
        /// <param name="bookId">Идентификатор книги.</param>
        /// <returns>Массив байт изображения книги, если оно найдено.</returns>
        /// <exception cref="NotFoundException">Если изображение книги не найдено.</exception>
        public async Task<byte[]?> ExecuteAsync(Guid bookId)
        {
            var imageData = await _imageService.GetImageAsync(bookId);
            if (imageData == null)
            {
                throw new NotFoundException("Book image not found.");
            }
            return imageData;
        }
    }
