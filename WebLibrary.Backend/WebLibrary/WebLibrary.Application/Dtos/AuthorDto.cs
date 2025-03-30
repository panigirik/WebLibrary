namespace WebLibrary.Application.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для представления информации об авторе.
/// </summary>
public class AuthorDto
{
    /// <summary>
    /// Уникальный идентификатор автора.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Имя автора.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Дата рождения автора.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Страна происхождения автора.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Коллекция книг, написанных автором.
    /// </summary>
    public ICollection<BookDto> Books { get; set; }
}