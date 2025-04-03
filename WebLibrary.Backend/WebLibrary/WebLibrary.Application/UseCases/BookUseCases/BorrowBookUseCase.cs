using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для заимствования книги.
    /// </summary>
    public class BorrowBookUseCase : IBorrowBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BorrowBookUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        public BorrowBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Выполняет заимствование книги пользователем.
        /// </summary>
        /// <param name="bookId">Идентификатор книги, которую пользователь хочет взять.</param>
        /// <param name="userId">Идентификатор пользователя, который заимствует книгу.</param>
        /// <returns>Возвращает <c>true</c>, если книга была успешно заимствована, иначе <c>false</c>.</returns>
        /// <exception cref="NotFoundException">
        /// Выбрасывается, если книга не найдена или она недоступна для заимствования.
        /// </exception>
        public async Task<bool> ExecuteAsync(Guid bookId, Guid userId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null || !book.IsAvailable)
            {
                throw new NotFoundException("Book not found");
            }

            book.IsAvailable = false;
            book.BorrowedById = userId;
            book.BorrowedAt = DateTime.UtcNow;
            book.ReturnBy = DateTime.UtcNow.AddDays(14);

            await _bookRepository.UpdateAsync(book);
            return true;
        }
    }
