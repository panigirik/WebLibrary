using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Enums;

namespace WebLibrary.Domain.Interfaces;

/// <summary>
/// Интерфейс для работы с токенами.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Генерирует acess-token.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <param name="role">Роль пользователя.</param>
    /// <returns>Уведомление.</returns>
    string GenerateAccessToken(Guid userId, Roles role);
    
    /// <summary>
    /// Обновляет refresh-token.
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    /// <returns>Уведомление.</returns>
    RefreshToken GenerateRefreshToken(Guid userId);
}