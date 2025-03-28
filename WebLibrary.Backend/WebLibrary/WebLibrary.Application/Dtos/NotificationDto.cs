namespace WebLibrary.Application.Dtos;

public class NotificationDto
{
    public Guid NotificationId { get; set; } // Уникальный идентификатор
    public Guid? UserId { get; set; } // Пользователь, кому отправлено уведомление
    
    public Guid BookId { get; set; } // id книги котору надо вернуть
    public string Message { get; set; } // Текст уведомления
    public DateTime CreatedAt { get; set; } // Дата создания
    public bool IsRead { get; set; } // Прочитано ли уведомление
}