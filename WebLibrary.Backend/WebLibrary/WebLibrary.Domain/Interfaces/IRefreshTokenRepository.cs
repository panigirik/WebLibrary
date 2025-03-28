using WebLibrary.Domain.Entities;

namespace WebLibrary.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetByIdAsync(Guid id);
    Task<IEnumerable<RefreshToken>> GetAllAsync();
    Task AddAsync(RefreshToken refreshToken);
    Task UpdateAsync(RefreshToken refreshToken);
    Task DeleteAsync(Guid id);
    Task<RefreshToken> GetByUserIdAsync(Guid userId);
}