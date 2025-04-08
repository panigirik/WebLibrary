using WebLibrary.Domain.Entities;

namespace WebLibrary.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с авторами.
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// Получить автора по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор автора.</param>
        /// <returns>Автор.</returns>
        Task<Author> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить всех авторов.
        /// </summary>
        /// <returns>Список авторов.</returns>
        IEnumerable<Author> GetAllAsync();

        /// <summary>
        /// Добавить нового автора.
        /// </summary>
        /// <param name="author">Автор для добавления.</param>
        Task AddAsync(Author author);

        /// <summary>
        /// Обновить информацию о существующем авторе.
        /// </summary>
        /// <param name="author">Автор с обновлёнными данными.</param>
        Task UpdateAsync(Author author);

        /// <summary>
        /// Удалить автора по уникальному идентификатору.
        /// </summary>
        /// <param name="author">Entity автора, которого надо удалить.</param>
        Task DeleteAsync(Author author);
    }
}