using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

public interface IGetNotificationByIdUseCase
{
    public Task<NotificationDto?> ExecuteAsync(Guid id);
}