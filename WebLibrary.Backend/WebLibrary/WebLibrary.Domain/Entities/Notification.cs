namespace WebLibrary.Domain.Entities;

public class Notification
{
    public Guid NotificationId { get; set; } // Уникальный идентификатор
    public Guid UserId { get; set; } // Пользователь, кому отправлено уведомление
    public User User { get; set; } // Навигационное свойство
    public string Message { get; set; } // Текст уведомления
    public DateTime CreatedAt { get; set; } // Дата создания
    public bool IsRead { get; set; } // Прочитано ли уведомление
}
