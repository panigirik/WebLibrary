using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

public interface IAddNotificationUseCase
{
    public Task ExecuteAsync(NotificationDto notificationDto);
}