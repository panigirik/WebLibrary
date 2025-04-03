namespace WebLibrary.Application.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при конфликте (409).
    /// </summary>
    public class ConflictException : Exception
    {
        /// <summary>
        /// Инициализирует новое исключение ConflictException с заданным сообщением.
        /// </summary>
        /// <param name="message">Сообщение, объясняющее причину ошибки.</param>
        public ConflictException(string message) : base(message)
        {
        }
    }
}