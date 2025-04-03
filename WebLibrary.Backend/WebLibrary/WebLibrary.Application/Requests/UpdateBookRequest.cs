using System.ComponentModel.DataAnnotations;

namespace WebLibrary.Application.Requests;

    /// <summary>
    /// DTO для обновления информации о книге.
    /// </summary>
    public class UpdateBookRequest
    {
        
        /// <summary>
        /// Идентификатор пользователя, который обновляет книгу.
        /// </summary>
        public Guid BookId { get; set; }
        
        /// <summary>
        /// ISBN книги.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Жанр.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }


    }

