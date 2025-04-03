using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.ImageInterfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для получения изображения книги.
    /// </summary>
    public class GetBookImageUseCase : IGetBookImageUseCase
    {
        private readonly IGetImageUseCase _getImageUseCase;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetBookImageUseCase"/>.
        /// </summary>
        /// <param name="getImageUseCase">Сервис для получения изображения книги.</param>
        public GetBookImageUseCase(IGetImageUseCase getImageUseCase)
        {
            _getImageUseCase = getImageUseCase;
        }

        /// <summary>
        /// Получает изображение книги по идентификатору книги.
        /// </summary>
        /// <param name="bookId">Идентификатор книги.</param>
        /// <returns>Массив байт изображения книги, если оно найдено.</returns>
        /// <exception cref="NotFoundException">Если изображение книги не найдено.</exception>
        public async Task<byte[]?> ExecuteAsync(Guid bookId)
        {
            var imageData = await _getImageUseCase.ExecuteAsync(bookId);
            if (imageData == null)
            {
                throw new NotFoundException("Book image not found.");
            }
            return imageData;
        }
    }
