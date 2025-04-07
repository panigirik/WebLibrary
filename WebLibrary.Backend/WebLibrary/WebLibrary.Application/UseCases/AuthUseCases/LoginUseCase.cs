using AutoMapper;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthUseCases;

    /// <summary>
    /// Use-case для входа пользователя в систему.
    /// </summary>
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILoginValidationService _validationUseCase;
        private readonly IMapper _mapper;
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoginUseCase"/>.
        /// </summary>
        /// <param name="jwtTokenService">Сервис для генерации JWT-токенов.</param>
        /// <param name="validationUseCase">Сервис валидации данных запроса.</param>
        /// <param name="emailUseCase">Use-case для получения пользователя по email.</param>
        /// <param name="addRefreshToken">Use-case для добавления refresh-токена.</param>
        /// <param name="mapper"> для маппинга сущностей.</param>
        public LoginUseCase(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenService jwtTokenService,
            ILoginValidationService validationUseCase,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _validationUseCase = validationUseCase;
            _mapper = mapper;
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

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid email or password");

            var accessToken = _jwtTokenService.GenerateAccessToken(user.UserId, user.Role);
            var refreshTokenDto = _jwtTokenService.GenerateRefreshToken(user.UserId);

            var refreshTokenEntity = _mapper.Map<RefreshToken>(refreshTokenDto);
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new LoginResult(accessToken, refreshTokenDto.Token);
        }

    }

