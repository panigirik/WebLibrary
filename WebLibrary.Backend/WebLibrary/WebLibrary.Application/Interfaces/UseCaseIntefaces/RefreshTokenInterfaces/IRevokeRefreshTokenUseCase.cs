namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

public interface IRevokeRefreshTokenUseCase
{
    Task ExecuteAsync(Guid id);
}