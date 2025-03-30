namespace WebLibrary.Domain.Entities
{
    /// <summary>
    /// Представляет автора книги.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Уникальный идентификатор автора.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Имя автора.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия автора.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения автора.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Страна происхождения автора.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Список книг, написанных автором.
        /// </summary>
        public ICollection<Book> Books { get; set; }
    }
}