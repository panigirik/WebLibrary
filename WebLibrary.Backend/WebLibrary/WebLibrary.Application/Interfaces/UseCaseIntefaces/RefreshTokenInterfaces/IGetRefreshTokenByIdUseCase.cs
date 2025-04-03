using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

public interface IGetRefreshTokenByIdUseCase
{
    Task<RefreshTokenDto?> ExecuteAsync(Guid id);
}