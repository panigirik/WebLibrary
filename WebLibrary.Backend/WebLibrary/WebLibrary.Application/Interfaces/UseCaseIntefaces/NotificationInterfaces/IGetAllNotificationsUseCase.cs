using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

public interface IGetAllNotificationsUseCase
{
    public Task<IEnumerable<NotificationDto>> ExecuteAsync();
}