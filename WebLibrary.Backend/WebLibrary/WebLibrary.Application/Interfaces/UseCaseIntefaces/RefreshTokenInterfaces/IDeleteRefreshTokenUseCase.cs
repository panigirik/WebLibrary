using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

public interface IDeleteRefreshTokenUseCase
{
    Task ExecuteAsync(RefreshTokenDto refreshTokenDto);
}
