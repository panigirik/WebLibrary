using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

public interface IGetRefreshTokenByUserIdUseCase
{
    Task<RefreshTokenDto?> ExecuteAsync(Guid userId);
}