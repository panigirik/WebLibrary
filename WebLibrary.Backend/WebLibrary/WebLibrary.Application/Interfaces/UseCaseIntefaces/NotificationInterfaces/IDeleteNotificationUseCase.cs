using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

public interface IDeleteNotificationUseCase
{
    public Task ExecuteAsync(NotificationDto notificationDto);
}