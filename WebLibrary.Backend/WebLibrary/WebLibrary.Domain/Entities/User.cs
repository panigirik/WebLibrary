using System.ComponentModel.DataAnnotations.Schema;

namespace WebLibrary.Domain.Entities;

public class User
{
    public Guid UserId { get; set; } // Уникальный идентификатор пользователя
    public string UserName { get; set; } // Логин пользователя
    public string Email { get; set; } // Почта пользователя
    public string PasswordHash { get; set; } // Хеш пароля
    public string Role { get; set; } // Роль (User, Admin)
    
    [NotMapped]
    public List<Guid> BorrowedBooksIds { get; set; } = new(); // Взятые книги
}
