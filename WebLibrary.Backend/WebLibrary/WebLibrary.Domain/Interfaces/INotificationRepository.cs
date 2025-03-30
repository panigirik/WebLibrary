using WebLibrary.Domain.Entities;

namespace WebLibrary.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с уведомлениями.
    /// </summary>
    public interface INotificationRepository
    {
        /// <summary>
        /// Получить уведомление по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор уведомления.</param>
        /// <returns>Уведомление.</returns>
        Task<Notification> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить все уведомления.
        /// </summary>
        /// <returns>Список всех уведомлений.</returns>
        Task<IEnumerable<Notification>> GetAllAsync();

        /// <summary>
        /// Добавить новое уведомление.
        /// </summary>
        /// <param name="notification">Уведомление для добавления.</param>
        Task AddAsync(Notification notification);

        /// <summary>
        /// Обновить информацию об уведомлении.
        /// </summary>
        /// <param name="notification">Обновлённое уведомление.</param>
        Task UpdateAsync(Notification notification);

        /// <summary>
        /// Удалить уведомление по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор уведомления.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получить уведомления по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список уведомлений для данного пользователя.</returns>
        Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId);
    }
}