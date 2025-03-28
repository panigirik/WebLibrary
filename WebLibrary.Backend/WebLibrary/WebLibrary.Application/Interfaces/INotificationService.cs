using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync();
    Task<NotificationDto?> GetNotificationByIdAsync(Guid id);
    Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(Guid userId);
    Task AddNotificationAsync(NotificationDto notification);
    Task MarkAsReadAsync(Guid id);
    Task DeleteNotificationAsync(Guid id);
}