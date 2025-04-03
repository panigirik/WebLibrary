using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для возврата книги.
    /// </summary>
    public class ReturnBookUseCase : IReturnBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReturnBookUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        public ReturnBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Возвращает книгу.
        /// </summary>
        /// <param name="bookId">Идентификатор книги, которую необходимо вернуть.</param>
        /// <param name="userId">Идентификатор пользователя, который возвращает книгу.</param>
        /// <returns><c>true</c>, если книга была успешно возвращена; иначе <c>false</c>.</returns>
        public async Task<bool> ExecuteAsync(Guid bookId, Guid userId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null || book.BorrowedById != userId)
            {
                throw new NotFoundException("book not found");
            }
                

            book.IsAvailable = true;
            book.BorrowedById = null;
            book.BorrowedAt = null;
            book.ReturnBy = null;

            await _bookRepository.UpdateAsync(book);
            return true;
        }
    }
