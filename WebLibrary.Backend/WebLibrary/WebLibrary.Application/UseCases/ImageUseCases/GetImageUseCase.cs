using WebLibrary.Application.Interfaces.UseCaseIntefaces.ImageInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.ImageUseCases;

    /// <summary>
    /// Use Case для получения изображения книги.
    /// </summary>
    public class GetImageUseCase : IGetImageUseCase
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetImageUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        public GetImageUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Получает изображение книги по идентификатору.
        /// </summary>
        /// <param name="bookId">Идентификатор книги.</param>
        /// <returns>Изображение книги в виде массива байтов, если оно существует, иначе <c>null</c>.</returns>
        public async Task<byte[]?> ExecuteAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            return book?.ImageData;
        }
    }
