namespace WebLibrary.Application.Dtos;

public class GetBookRequestDto
{
    public Guid BookId { get; set; } // Уникальный идентификатор книги
    public string ISBN { get; set; } // Международный стандартный книжный номер
    public string Title { get; set; } // Название книги
    public string Genre { get; set; } // Жанр
    public string Description { get; set; } // Описание книги
    public Guid AuthorId { get; set; } // Связь с автором
    
    public DateTime? BorrowedAt { get; set; } // Время, когда книгу взяли
    public DateTime? ReturnBy { get; set; } // Время, когда книгу надо вернуть
    public Guid? BorrowedById { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public byte[]? ImageData { get; set; } // Бинарные данные изображения
}
