using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.RefreshTokenUseCases;

    /// <summary>
    /// Use-case для получения refresh-токена по идентификатору.
    /// </summary>
    public class GetRefreshTokenByIdUseCase : IGetRefreshTokenByIdUseCase
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetRefreshTokenByIdUseCase"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с refresh-токенами.</param>
        /// <param name="mapper">Маппер для преобразования сущностей в DTO.</param>
        public GetRefreshTokenByIdUseCase(IRefreshTokenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает refresh-токен по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор refresh-токена.</param>
        /// <returns>DTO объект refresh-токена.</returns>
        /// <exception cref="NotFoundException">Выбрасывается, если токен не найден.</exception>
        public async Task<RefreshTokenDto?> ExecuteAsync(Guid id)
        {
            var token = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Token not found");
            return _mapper.Map<RefreshTokenDto>(token);
        }
    }
