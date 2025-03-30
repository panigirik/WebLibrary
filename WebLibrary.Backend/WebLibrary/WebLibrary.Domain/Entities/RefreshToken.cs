namespace WebLibrary.Domain.Entities
{
    /// <summary>
    /// Представляет токен обновления для пользователя.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Уникальный идентификатор токена обновления.
        /// </summary>
        public Guid RefreshTokenId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит токен.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Сам токен обновления.
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
}