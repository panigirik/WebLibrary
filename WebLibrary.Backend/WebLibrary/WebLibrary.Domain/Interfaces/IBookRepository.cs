using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с книгами.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Получить книгу по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги.</param>
        /// <returns>Книга.</returns>
        Task<Book> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить книгу по ISBN.
        /// </summary>
        /// <param name="isbn">ISBN книги.</param>
        /// <returns>Книга.</returns>
        Task<Book> GetByIsbnAsync(string isbn);

        /// <summary>
        /// Получить книги с пагинацией, используя фильтр.
        /// </summary>
        /// <param name="filter">Фильтр для пагинации.</param>
        /// <returns>Список книг с пагинацией.</returns>
        Task<List<Book>> GetPaginated(PaginatedBookFilter filter);

        /// <summary>
        /// Получить все книги.
        /// </summary>
        /// <returns>Список всех книг.</returns>
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>
        /// Добавить новую книгу.
        /// </summary>
        /// <param name="book">Книга для добавления.</param>
        Task AddAsync(Book book);

        /// <summary>
        /// Обновить информацию о книге.
        /// </summary>
        /// <param name="book">Обновлённая книга.</param>
        Task UpdateAsync(Book book);

        /// <summary>
        /// Удалить книгу по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получить книги по идентификатору автора.
        /// </summary>
        /// <param name="authorId">Идентификатор автора.</param>
        /// <returns>Список книг автора.</returns>
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId);

        /// <summary>
        /// Получить просроченные книги.
        /// </summary>
        /// <returns>Список просроченных книг.</returns>
        Task<IEnumerable<Book>> GetOverdueBooksAsync();
    }
}
