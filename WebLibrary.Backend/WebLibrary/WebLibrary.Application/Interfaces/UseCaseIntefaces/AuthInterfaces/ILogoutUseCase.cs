namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;

/// <summary>
/// Интерфейс для выхода пользователя.
/// </summary>
public interface ILogoutUseCase
{
    Task ExecuteAsync(Guid userId);
}