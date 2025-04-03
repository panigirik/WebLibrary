namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

public interface IDeleteRefreshTokenUseCase
{
    Task ExecuteAsync(Guid id);
}
