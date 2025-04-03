using WebLibrary.Application.Dtos;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Application.Interfaces;

/// <summary>
/// Интерфейс для сервиса управления книгами.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Получает список всех книг.
    /// </summary>
    Task<IEnumerable<GetBookRequestDto>> GetAllBooksAsync();

    /// <summary>
    /// Получает книгу по идентификатору.
    /// </summary>
    Task<GetBookRequestDto?> GetBookByIdAsync(Guid id);

    /// <summary>
    /// Получает книгу по ISBN.
    /// </summary>
    Task<GetBookRequestDto?> GetBookByIsbnAsync(string isbn);

    /// <summary>
    /// Получает список книг по идентификатору автора.
    /// </summary>
    Task<IEnumerable<GetBookRequestDto>> GetBooksByAuthorAsync(Guid authorId);

    /// <summary>
    /// Получает список книг с пагинацией.
    /// </summary>
    Task<IEnumerable<GetBookRequestDto>> GetPaginatedBooksAsync(PaginatedBookFilter filter);

    /// <summary>
    /// Добавляет новую книгу.
    /// </summary>
    Task AddBookAsync(AddBookRequest bookRequest);

    /// <summary>
    /// Обновляет информацию о книге.
    /// </summary>
    Task UpdateBookAsync(BookDto bookDto);

    /// <summary>
    /// Удаляет книгу по идентификатору.
    /// </summary>
    Task DeleteBookAsync(Guid id);

    /// <summary>
    /// Оформляет книгу в аренду пользователем.
    /// </summary>
    Task<bool> BorrowBookAsync(Guid bookId, Guid userId);

    /// <summary>
    /// Возвращает арендованную книгу.
    /// </summary>
    Task<bool> ReturnBookAsync(Guid bookId, Guid userId);

    /// <summary>
    /// Получает изображение книги.
    /// </summary>
    Task<byte[]?> GetBookImageAsync(Guid bookId);
}