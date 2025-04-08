using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace WebLibrary.Domain.Entities
{
    /// <summary>
    /// Представляет книгу.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Уникальный идентификатор книги.
        /// </summary>
        public Guid BookId { get; set; } = new Guid();

        /// <summary>
        /// Международный стандартный книжный номер (ISBN).
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Название книги.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Жанр книги.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Описание книги.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор автора книги.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Изображение книги в виде байтов.
        /// </summary>
        [JsonIgnore] 
        public byte[]? ImageData { get; set; }

        /// <summary>
        /// Время, когда книгу взяли.
        /// </summary>
        public DateTime? BorrowedAt { get; set; }

        /// <summary>
        /// Время, когда книгу нужно вернуть.
        /// </summary>
        public DateTime? ReturnBy { get; set; }

        /// <summary>
        /// Идентификатор пользователя, который взял книгу.
        /// </summary>
        public Guid? BorrowedById { get; set; }

        /// <summary>
        /// Связь с пользователем, который взял книгу.
        /// </summary>
        public User? BorrowedBy { get; set; }

        /// <summary>
        /// Файл изображения книги.
        /// </summary>
        [JsonIgnore] [NotMapped]
        public IFormFile? ImageFile { get; set; }

        /// <summary>
        /// Флаг, указывающий, доступна ли книга для заимствования.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
