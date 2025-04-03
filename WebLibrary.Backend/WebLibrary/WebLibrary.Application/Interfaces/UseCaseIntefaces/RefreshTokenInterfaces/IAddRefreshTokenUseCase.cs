using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

public interface IAddRefreshTokenUseCase
{
    Task ExecuteAsync(RefreshTokenDto refreshTokenDto);
}