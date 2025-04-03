using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.RefreshTokenUseCases;

    /// <summary>
    /// Use-case для получения всех refresh-токенов.
    /// </summary>
    public class GetAllRefreshTokensUseCase : IGetAllRefreshTokensUseCase
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetAllRefreshTokensUseCase"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с refresh-токенами.</param>
        /// <param name="mapper">Маппер для преобразования сущностей в DTO.</param>
        public GetAllRefreshTokensUseCase(IRefreshTokenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает все refresh-токены.
        /// </summary>
        /// <returns>Список DTO объектов всех refresh-токенов.</returns>
        public async Task<IEnumerable<RefreshTokenDto>> ExecuteAsync()
        {
            var tokens = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RefreshTokenDto>>(tokens);
        }
    }
