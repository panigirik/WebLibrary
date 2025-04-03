using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

namespace WebLibrary.Application.UseCases.AuthUseCases;

    /// <summary>
    /// Use-case для выхода пользователя из системы.
    /// </summary>
    public class LogoutUseCase : ILogoutUseCase
    {
        private readonly IRevokeRefreshTokenUseCase _revokeRefreshToken;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LogoutUseCase"/>.
        /// </summary>
        /// <param name="revokeRefreshToken">Use-case для отзыва refresh-токена пользователя.</param>
        public LogoutUseCase(IRevokeRefreshTokenUseCase revokeRefreshToken)
        {
            _revokeRefreshToken = revokeRefreshToken;
        }

        /// <summary>
        /// Выполняет выход пользователя из системы, отзывая его refresh-токен.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Задача, представляющая процесс выхода.</returns>
        public async Task ExecuteAsync(Guid userId)
        {
            await _revokeRefreshToken.ExecuteAsync(userId);
        }
    }
