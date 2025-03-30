using System.ComponentModel.DataAnnotations.Schema;
using WebLibrary.Domain.Enums;

namespace WebLibrary.Domain.Entities
{
    /// <summary>
    /// Представляет пользователя системы.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хеш пароля пользователя.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Роль пользователя (например, User или Admin).
        /// </summary>
        public Roles Role { get; set; }

        /// <summary>
        /// Список книг, взятых пользователем.
        /// </summary>
        public List<Book> BorrowedBooks { get; set; } = new();
    }
}