namespace WebLibrary.Domain.Entities;

public class Author
{
    public Guid AuthorId { get; set; } // Уникальный идентификатор
    public string FirstName { get; set; } // Имя автора
    public string LastName { get; set; } // Фамилия автора
    public DateTime DateOfBirth { get; set; } // Дата рождения
    public string Country { get; set; } // Страна происхождения
    public ICollection<Book> Books { get; set; } // Связь с книгами
}
