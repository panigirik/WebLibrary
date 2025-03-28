using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces;

public interface IRefreshTokenService
{
    Task<IEnumerable<RefreshTokenDto>> GetAllRefreshTokensAsync();
    Task<RefreshTokenDto?> GetRefreshTokenByIdAsync(Guid id);
    Task<RefreshTokenDto?> GetRefreshTokenByUserIdAsync(Guid userId);
    Task AddRefreshTokenAsync(RefreshTokenDto refreshToken);
    Task RevokeRefreshTokenAsync(Guid id);
    Task DeleteRefreshTokenAsync(Guid id);
}