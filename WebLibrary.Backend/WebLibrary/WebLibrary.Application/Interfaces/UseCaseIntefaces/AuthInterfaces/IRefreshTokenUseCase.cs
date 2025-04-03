namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;

/// <summary>
/// Интерфейс для обновления токена.
/// </summary>
public interface IRefreshTokenUseCase
{
    Task<string> ExecuteAsync(Guid userId, string refreshToken);
}