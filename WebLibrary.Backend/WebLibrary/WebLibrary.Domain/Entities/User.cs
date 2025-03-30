using System.ComponentModel.DataAnnotations.Schema;
using WebLibrary.Domain.Enums;

namespace WebLibrary.Domain.Entities;

public class User
{
    public Guid UserId { get; set; } // Уникальный идентификатор пользователя
    public string UserName { get; set; } // Логин пользователя
    public string Email { get; set; } // Почта пользователя
    public string PasswordHash { get; set; } // Хеш пароля
    public Roles Role { get; set; } // Роль (User, Admin)
    
    public List<Book> BorrowedBooks { get; set; } = new(); // Взятые книги
}
