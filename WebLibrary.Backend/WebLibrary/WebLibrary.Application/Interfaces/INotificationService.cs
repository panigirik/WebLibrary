using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces;

/// <summary>
/// Интерфейс для сервиса управления уведомлениями.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Получает все уведомления.
    /// </summary>
    Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync();

    /// <summary>
    /// Получает уведомление по идентификатору.
    /// </summary>
    Task<NotificationDto?> GetNotificationByIdAsync(Guid id);

    /// <summary>
    /// Получает уведомления пользователя по его идентификатору.
    /// </summary>
    Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(Guid userId);

    /// <summary>
    /// Добавляет новое уведомление.
    /// </summary>
    Task AddNotificationAsync(NotificationDto notification);

    /// <summary>
    /// Отмечает уведомление как прочитанное.
    /// </summary>
    Task MarkAsReadAsync(Guid id);

    /// <summary>
    /// Удаляет уведомление по идентификатору.
    /// </summary>
    Task DeleteNotificationAsync(Guid id);
}