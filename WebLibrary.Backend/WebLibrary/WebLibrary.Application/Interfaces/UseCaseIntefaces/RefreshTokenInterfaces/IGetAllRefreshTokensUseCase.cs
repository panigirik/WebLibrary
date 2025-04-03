using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

public interface IGetAllRefreshTokensUseCase
{
    Task<IEnumerable<RefreshTokenDto>> ExecuteAsync();
}