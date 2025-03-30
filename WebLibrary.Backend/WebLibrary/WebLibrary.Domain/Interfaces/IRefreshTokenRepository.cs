using WebLibrary.Domain.Entities;

namespace WebLibrary.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с refresh токенами.
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// Получить refresh токен по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор токена.</param>
        /// <returns>Refresh токен.</returns>
        Task<RefreshToken> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить все refresh токены.
        /// </summary>
        /// <returns>Список всех refresh токенов.</returns>
        Task<IEnumerable<RefreshToken>> GetAllAsync();

        /// <summary>
        /// Добавить новый refresh токен.
        /// </summary>
        /// <param name="refreshToken">Refresh токен для добавления.</param>
        Task AddAsync(RefreshToken refreshToken);

        /// <summary>
        /// Обновить информацию о refresh токене.
        /// </summary>
        /// <param name="refreshToken">Обновлённый refresh токен.</param>
        Task UpdateAsync(RefreshToken refreshToken);

        /// <summary>
        /// Удалить refresh токен по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор токена.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получить refresh токен по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Refresh токен пользователя.</returns>
        Task<RefreshToken> GetByUserIdAsync(Guid userId);
    }
}