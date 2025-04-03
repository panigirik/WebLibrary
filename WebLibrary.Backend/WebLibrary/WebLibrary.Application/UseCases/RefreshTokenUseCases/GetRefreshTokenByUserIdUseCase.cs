using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.RefreshTokenUseCases;

    /// <summary>
    /// Use-case для получения refresh-токена по идентификатору пользователя.
    /// </summary>
    public class GetRefreshTokenByUserIdUseCase : IGetRefreshTokenByUserIdUseCase
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetRefreshTokenByUserIdUseCase"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с refresh-токенами.</param>
        /// <param name="mapper">Маппер для преобразования сущностей в DTO.</param>
        public GetRefreshTokenByUserIdUseCase(IRefreshTokenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает refresh-токен по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>DTO объект refresh-токена.</returns>
        /// <exception cref="NotFoundException">Выбрасывается, если токен для данного пользователя не найден.</exception>
        public async Task<RefreshTokenDto?> ExecuteAsync(Guid userId)
        {
            var token = await _repository.GetByUserIdAsync(userId) ?? throw new NotFoundException("Token not found");
            return _mapper.Map<RefreshTokenDto>(token);
        }
    }
