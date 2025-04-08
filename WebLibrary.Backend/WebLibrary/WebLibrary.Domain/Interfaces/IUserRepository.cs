using WebLibrary.Domain.Entities;

namespace WebLibrary.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получить пользователя по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Пользователь.</returns>
        Task<User> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Добавить нового пользователя.
        /// </summary>
        /// <param name="user">Пользователь для добавления.</param>
        Task AddAsync(User user);

        /// <summary>
        /// Обновить информацию о пользователе.
        /// </summary>
        /// <param name="user">Пользователь с обновлёнными данными.</param>
        Task UpdateAsync(User user);

        /// <summary>
        /// Удалить пользователя по уникальному идентификатору.
        /// </summary>
        /// <param name="user">Entity пользователя, которого надо удалить.</param>
        Task DeleteAsync(User user);

        /// <summary>
        /// Получить пользователя по email.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <returns>Пользователь.</returns>
        Task<User> GetByEmailAsync(string email);
    }
}