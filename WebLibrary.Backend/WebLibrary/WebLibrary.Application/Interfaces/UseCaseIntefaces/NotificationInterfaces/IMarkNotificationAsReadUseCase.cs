namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

public interface IMarkNotificationAsReadUseCase
{
    public Task ExecuteAsync(Guid id);
}