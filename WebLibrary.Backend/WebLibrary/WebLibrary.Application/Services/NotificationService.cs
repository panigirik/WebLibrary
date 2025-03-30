using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

/// <summary>
/// Сервис для управления уведомлениями.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="NotificationService"/>.
    /// </summary>
    /// <param name="notificationRepository">Репозиторий уведомлений.</param>
    /// <param name="mapper">Маппер объектов.</param>
    public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает все уведомления.
    /// </summary>
    /// <returns>Список уведомлений.</returns>
    public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync()
    {
        var notifications = await _notificationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    /// <summary>
    /// Получает уведомление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор уведомления.</param>
    /// <returns>DTO уведомления или null.</returns>
    public async Task<NotificationDto?> GetNotificationByIdAsync(Guid id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        return _mapper.Map<NotificationDto?>(notification);
    }

    /// <summary>
    /// Получает уведомления по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Список уведомлений пользователя.</returns>
    public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(Guid userId)
    {
        var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    /// <summary>
    /// Добавляет новое уведомление.
    /// </summary>
    /// <param name="notificationDto">DTO уведомления.</param>
    public async Task AddNotificationAsync(NotificationDto notificationDto)
    {
        var notification = _mapper.Map<Notification>(notificationDto);
        await _notificationRepository.AddAsync(notification);
    }

    /// <summary>
    /// Помечает уведомление как прочитанное.
    /// </summary>
    /// <param name="id">Идентификатор уведомления.</param>
    public async Task MarkAsReadAsync(Guid id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        if (notification != null)
        {
            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification);
        }
    }

    /// <summary>
    /// Удаляет уведомление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор уведомления.</param>
    public async Task DeleteNotificationAsync(Guid id)
    {
        await _notificationRepository.DeleteAsync(id);
    }
}