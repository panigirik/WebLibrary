using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace WebLibrary.Domain.Entities;

public class Book
{
    public Guid BookId { get; set; } // Уникальный идентификатор книги
    public string ISBN { get; set; } // Международный стандартный книжный номер
    public string Title { get; set; } // Название книги
    public string Genre { get; set; } // Жанр
    public string Description { get; set; } // Описание книги
    public Guid AuthorId { get; set; } // Связь с автором
    
    [JsonIgnore] 
    public byte[]? ImageData { get; set; }
    public DateTime? BorrowedAt { get; set; } // Время, когда книгу взяли
    public DateTime? ReturnBy { get; set; } // Время, когда книгу надо вернуть
    public Guid? BorrowedById { get; set; }
    
    public User? BorrowedBy { get; set; }

    [JsonIgnore] [NotMapped]
    public IFormFile? ImageFile { get; set; } 
    public bool IsAvailable { get; set; }
}

