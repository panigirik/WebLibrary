namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

public interface IDeleteNotificationUseCase
{
    public Task ExecuteAsync(Guid id);
}