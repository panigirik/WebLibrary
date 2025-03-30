namespace WebLibrary.Domain.Entities;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; } // Уникальный идентификатор
    public Guid UserId { get; set; } // Пользователь, которому принадлежит токен
   public string Token { get; set; } // Сам токен
    public DateTime Expires { get; set; } // Дата истечения срока действия
    public bool IsRevoked { get; set; } // Флаг отзыва токена
}
