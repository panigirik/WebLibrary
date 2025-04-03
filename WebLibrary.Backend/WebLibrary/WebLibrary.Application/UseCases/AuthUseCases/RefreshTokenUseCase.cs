using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthUseCases;

    /// <summary>
    /// Use-case для обновления токена доступа.
    /// </summary>
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly IGetUserByIdUseCase _case;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IGetRefreshTokenByUserIdUseCase _getRefreshToken;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RefreshTokenUseCase"/>.
        /// </summary>
        /// <param name="jwtTokenService">Сервис для генерации JWT-токенов.</param>
        /// <param name="getRefreshToken">Use-case для получения refresh-токена пользователя.</param>
        public RefreshTokenUseCase(IJwtTokenService jwtTokenService, 
            IGetRefreshTokenByUserIdUseCase getRefreshToken)
        {
            _jwtTokenService = jwtTokenService;
            _getRefreshToken = getRefreshToken;
        }

        /// <summary>
        /// Выполняет обновление access-токена на основе refresh-токена.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="refreshToken">Текущий refresh-токен пользователя.</param>
        /// <returns>Новый access-токен.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Выбрасывается, если refresh-токен недействителен, истек или пользователь не найден.
        /// </exception>
        public async Task<string> ExecuteAsync(Guid userId, string refreshToken)
        {
            var storedToken = await _getRefreshToken.ExecuteAsync(userId);
            if (storedToken == null || storedToken.Token != refreshToken || storedToken.IsRevoked || storedToken.Expires < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var user = await _case.ExecuteAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            return _jwtTokenService.GenerateAccessToken(user.UserId, user.RoleType);
        }
    }

