using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces;

/// <summary>
/// Интерфейс для сервиса управления авторами.
/// </summary>
public interface IAuthorService
{
    /// <summary>
    /// Получает список всех авторов.
    /// </summary>
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

    /// <summary>
    /// Получает информацию об авторе по его идентификатору.
    /// </summary>
    Task<AuthorDto?> GetAuthorByIdAsync(Guid id);

    /// <summary>
    /// Добавляет нового автора.
    /// </summary>
    Task AddAuthorAsync(AuthorDto author);

    /// <summary>
    /// Обновляет информацию об авторе.
    /// </summary>
    Task UpdateAuthorAsync(AuthorDto author);

    /// <summary>
    /// Удаляет автора по идентификатору.
    /// </summary>
    Task DeleteAuthorAsync(Guid id);

    /// <summary>
    /// Получает список книг данного автора.
    /// </summary>
    Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId);
}