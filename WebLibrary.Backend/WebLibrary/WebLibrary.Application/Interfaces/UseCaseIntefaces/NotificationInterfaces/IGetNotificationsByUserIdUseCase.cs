using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

public interface IGetNotificationsByUserIdUseCase
{
    public Task<IEnumerable<NotificationDto>> ExecuteAsync(Guid userId);
}