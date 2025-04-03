namespace WebLibrary.Application.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при попытке доступа, запрещённого пользователю (403).
    /// </summary>
    public class ForbiddenException : Exception
    {
        /// <summary>
        /// Инициализирует новое исключение ForbiddenException с заданным сообщением.
        /// </summary>
        /// <param name="message">Сообщение, объясняющее причину ошибки. По умолчанию "Access denied".</param>
        public ForbiddenException(string message = "Access denied") : base(message)
        {
        }
    }
}