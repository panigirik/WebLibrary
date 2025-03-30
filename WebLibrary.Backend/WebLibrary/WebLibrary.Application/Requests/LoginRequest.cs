namespace WebLibrary.Application.Requests
{
    /// <summary>
    /// Запрос для выполнения входа в систему.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }
    }
}