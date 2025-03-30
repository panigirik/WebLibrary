namespace WebLibrary.Application.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для представления уведомления.
/// </summary>
public class NotificationDto
{
    /// <summary>
    /// Уникальный идентификатор уведомления.
    /// </summary>
    public Guid NotificationId { get; set; }

    /// <summary>
    /// Идентификатор пользователя, кому отправлено уведомление.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Идентификатор книги, которую нужно вернуть.
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Текст уведомления.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Дата и время создания уведомления.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Статус уведомления: прочитано или нет.
    /// </summary>
    public bool IsRead { get; set; }
}