using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthUseCases;

    /// <summary>
    /// Use-case для входа пользователя в систему.
    /// </summary>
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IGetUserByEmailUseCase _emailUseCase;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAddRefreshTokenUseCase _addRefreshToken;
        private readonly IValidationUseCase _validationUseCase;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoginUseCase"/>.
        /// </summary>
        /// <param name="jwtTokenService">Сервис для генерации JWT-токенов.</param>
        /// <param name="validationUseCase">Сервис валидации данных запроса.</param>
        /// <param name="emailUseCase">Use-case для получения пользователя по email.</param>
        /// <param name="addRefreshToken">Use-case для добавления refresh-токена.</param>
        public LoginUseCase(IJwtTokenService jwtTokenService, 
            IValidationUseCase validationUseCase,
            IGetUserByEmailUseCase emailUseCase,
            IAddRefreshTokenUseCase addRefreshToken)
        {
            _jwtTokenService = jwtTokenService;
            _validationUseCase = validationUseCase;
            _emailUseCase = emailUseCase;
            _addRefreshToken = addRefreshToken;
        }

        /// <summary>
        /// Выполняет аутентификацию пользователя.
        /// </summary>
        /// <param name="request">Объект <see cref="LoginRequest"/>, содержащий данные для входа.</param>
        /// <returns>Возвращает объект <see cref="LoginResult"/>, содержащий access и refresh токены.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Выбрасывается, если входные данные некорректны или учетные данные неверны.
        /// </exception>
        public async Task<LoginResult> ExecuteAsync(LoginRequest request)
        {
            var validationResult = await _validationUseCase.ValidateLoginRequestAsync(request);
            if (!validationResult.IsValid)
                throw new UnauthorizedException("Invalid login request");

            var user = await _emailUseCase.ExecuteAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid email or password");

            var accessToken = _jwtTokenService.GenerateAccessToken(user.UserId, user.RoleType);
            var refreshToken = _jwtTokenService.GenerateRefreshToken(user.UserId);

            await _addRefreshToken.ExecuteAsync(new RefreshTokenDto
            {
                RefreshTokenId = refreshToken.RefreshTokenId,
                UserId = user.UserId,
                Token = refreshToken.Token,
                Expires = refreshToken.Expires,
                IsRevoked = refreshToken.IsRevoked
            });

            return new LoginResult(accessToken, refreshToken.Token);
        }
    }

