using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces;

/// <summary>
/// Интерфейс для сервиса управления токенами обновления.
/// </summary>
public interface IRefreshTokenService
{
    /// <summary>
    /// Получает все токены обновления.
    /// </summary>
    Task<IEnumerable<RefreshTokenDto>> GetAllRefreshTokensAsync();

    /// <summary>
    /// Получает токен обновления по его идентификатору.
    /// </summary>
    Task<RefreshTokenDto?> GetRefreshTokenByIdAsync(Guid id);

    /// <summary>
    /// Получает токен обновления по идентификатору пользователя.
    /// </summary>
    Task<RefreshTokenDto?> GetRefreshTokenByUserIdAsync(Guid userId);

    /// <summary>
    /// Добавляет новый токен обновления.
    /// </summary>
    Task AddRefreshTokenAsync(RefreshTokenDto refreshToken);

    /// <summary>
    /// Отзывает (аннулирует) токен обновления.
    /// </summary>
    Task RevokeRefreshTokenAsync(Guid id);

    /// <summary>
    /// Удаляет токен обновления по его идентификатору.
    /// </summary>
    Task DeleteRefreshTokenAsync(Guid id);
}