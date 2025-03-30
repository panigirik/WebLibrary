namespace WebLibrary.Domain.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при некорректном запросе (400).
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Инициализирует новое исключение BadRequestException с заданным сообщением.
        /// </summary>
        /// <param name="message">Сообщение, объясняющее причину ошибки.</param>
        public BadRequestException(string message) : base("Bad request")
        {
        }
    }
}