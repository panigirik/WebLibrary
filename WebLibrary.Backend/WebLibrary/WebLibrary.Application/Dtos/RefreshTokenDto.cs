using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для представления обновляемого токена (refresh token).
/// </summary>
public class RefreshTokenDto
{
    /// <summary>
    /// Уникальный идентификатор обновляемого токена.
    /// </summary>
    public Guid RefreshTokenId { get; set; }

    /// <summary>
    /// Идентификатор пользователя, которому принадлежит токен.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Сам обновляемый токен.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Дата истечения срока действия токена.
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// Флаг, указывающий, был ли токен отозван.
    /// </summary>
    public bool IsRevoked { get; set; }
}