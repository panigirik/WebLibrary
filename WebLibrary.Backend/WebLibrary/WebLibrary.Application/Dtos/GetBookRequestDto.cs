namespace WebLibrary.Application.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для запроса информации о книге.
/// </summary>
public class GetBookRequestDto
{
    /// <summary>
    /// Уникальный идентификатор книги.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Международный стандартный книжный номер (ISBN).
    /// </summary>
    public string ISBN { get; set; }

    /// <summary>
    /// Название книги.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Жанр книги.
    /// </summary>
    public string Genre { get; set; }

    /// <summary>
    /// Описание книги.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор автора книги (связь с автором).
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Время, когда книгу взяли.
    /// </summary>
    public DateTime? BorrowedAt { get; set; }

    /// <summary>
    /// Время, когда книгу надо вернуть.
    /// </summary>
    public DateTime? ReturnBy { get; set; }

    /// <summary>
    /// Идентификатор пользователя, взявшего книгу.
    /// </summary>
    public Guid? BorrowedById { get; set; }

    /// <summary>
    /// Статус доступности книги (доступна/недоступна).
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Бинарные данные изображения книги.
    /// </summary>
    public byte[]? ImageData { get; set; }
}