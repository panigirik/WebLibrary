namespace WebLibrary.Domain.Entities
{
    /// <summary>
    /// Представляет уведомление для пользователя.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Уникальный идентификатор уведомления.
        /// </summary>
        public Guid NotificationId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому отправлено уведомление.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Навигационное свойство для связи с пользователем.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Текст уведомления.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Дата и время создания уведомления.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Флаг, указывающий, было ли прочитано уведомление.
        /// </summary>
        public bool IsRead { get; set; }
    }
}