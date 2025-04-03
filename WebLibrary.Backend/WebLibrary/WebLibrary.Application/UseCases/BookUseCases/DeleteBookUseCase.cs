using FluentValidation;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для удаления книги.
    /// </summary>
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteBookUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        public DeleteBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Выполняет удаление книги по её идентификатору.
        /// </summary>
        /// <param name="bookId">Идентификатор книги, которую необходимо удалить.</param>
        /// <exception cref="ValidationException">
        /// Выбрасывается, если книга с указанным идентификатором не найдена.
        /// </exception>
        public async Task ExecuteAsync(Guid bookId)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookId);
            if (existingBook == null)
            {
                throw new ValidationException("Book with this id not found");
            }

            await _bookRepository.DeleteAsync(bookId);
        }
    }
