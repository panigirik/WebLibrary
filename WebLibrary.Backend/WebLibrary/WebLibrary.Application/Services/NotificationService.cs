using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync()
    {
        var notifications = await _notificationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<NotificationDto?> GetNotificationByIdAsync(Guid id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        return _mapper.Map<NotificationDto?>(notification);
    }

    public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(Guid userId)
    {
        var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task AddNotificationAsync(NotificationDto notificationDto)
    {
        var notification = _mapper.Map<Notification>(notificationDto);
        await _notificationRepository.AddAsync(notification);
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        if (notification != null)
        {
            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification);
        }
    }

    public async Task DeleteNotificationAsync(Guid id)
    {
        await _notificationRepository.DeleteAsync(id);
    }
}