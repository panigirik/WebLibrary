using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
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
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RefreshTokenUseCase"/>.
        /// </summary>
        /// <param name="jwtTokenService">Сервис для генерации JWT-токенов.</param>
        /// <param name="userRepository">Сервис для генерации JWT-токенов.</param>
        /// <param name="refreshTokenRepository">Use-case для получения refresh-токена пользователя.</param>
        /// <param name="mapper">Use-case для получения refresh-токена пользователя.</param>
        public RefreshTokenUseCase(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
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
            var storedToken = await _refreshTokenRepository.GetByUserIdAsync(userId);

            if (storedToken == null || 
                storedToken.Token != refreshToken || 
                storedToken.IsRevoked || 
                storedToken.Expires < DateTime.UtcNow)
            {
                throw new UnauthorizedException("Invalid or expired refresh token");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedException("User not found");

            var userDto = _mapper.Map<UserDto>(user);

            return _jwtTokenService.GenerateAccessToken(userDto.UserId, userDto.RoleType);
        }
    }

