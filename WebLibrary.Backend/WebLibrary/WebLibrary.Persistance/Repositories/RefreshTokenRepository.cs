using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using WebLibrary.Persistance;

/// <summary>
/// Репозиторий для работы с refresh токенами.
/// </summary>
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="RefreshTokenRepository"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает refresh токен по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор refresh токена.</param>
    /// <returns>Refresh токен с указанным идентификатором.</returns>
    public async Task<RefreshToken> GetByIdAsync(Guid id)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.RefreshTokenId == id);
    }

    /// <summary>
    /// Получает все refresh токены.
    /// </summary>
    /// <returns>Список всех refresh токенов.</returns>
    public async Task<IEnumerable<RefreshToken>> GetAllAsync()
    {
        return await _context.RefreshTokens.ToListAsync();
    }

    /// <summary>
    /// Добавляет новый refresh токен.
    /// </summary>
    /// <param name="refreshToken">Refresh токен для добавления.</param>
    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет существующий refresh токен.
    /// </summary>
    /// <param name="refreshToken">Refresh токен для обновления.</param>
    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет refresh токен по идентификатору.
    /// </summary>
    /// <param name="token">Dto refresh-токена для удаления.</param>
    public async Task DeleteAsync(RefreshToken token)
    {
        _context.RefreshTokens.Remove(token);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Получает refresh токен по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Refresh токен для пользователя.</returns>
    public async Task<RefreshToken> GetByUserIdAsync(Guid userId)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId);
    }
}
