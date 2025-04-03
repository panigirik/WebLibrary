using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;

/// <summary>
/// Интерфейс для обработки входа пользователя.
/// </summary>
public interface ILoginUseCase
{
    Task<LoginResult> ExecuteAsync(LoginRequest request);
}