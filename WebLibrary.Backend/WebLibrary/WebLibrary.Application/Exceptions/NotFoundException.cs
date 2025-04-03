namespace WebLibrary.Application.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при отсутствии ресурса (404).
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Инициализирует новое исключение NotFoundException с заданным сообщением.
        /// </summary>
        /// <param name="message">Сообщение, объясняющее причину ошибки.</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}