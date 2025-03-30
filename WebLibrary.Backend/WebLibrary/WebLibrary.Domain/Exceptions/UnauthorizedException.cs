namespace WebLibrary.Domain.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при попытке неавторизованного доступа (401).
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Инициализирует новое исключение UnauthorizedException с заданным сообщением.
        /// </summary>
        /// <param name="message">Сообщение, объясняющее причину ошибки. По умолчанию "Unauthorized".</param>
        public UnauthorizedException(string message = "Unauthorized") : base(message)
        {
        }
    }
}