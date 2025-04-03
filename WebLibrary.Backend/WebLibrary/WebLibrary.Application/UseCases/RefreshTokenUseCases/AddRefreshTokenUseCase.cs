using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.RefreshTokenUseCases;

    /// <summary>
    /// Use-case для добавления нового refresh-токена.
    /// </summary>
    public class AddRefreshTokenUseCase : IAddRefreshTokenUseCase
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddRefreshTokenUseCase"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с refresh-токенами.</param>
        /// <param name="mapper">Маппер для преобразования DTO в сущности.</param>
        public AddRefreshTokenUseCase(IRefreshTokenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавляет новый refresh-токен в хранилище.
        /// </summary>
        /// <param name="refreshTokenDto">DTO с данными для добавления нового refresh-токена.</param>
        /// <returns>Задача, представляющая асинхронную операцию добавления.</returns>
        public async Task ExecuteAsync(RefreshTokenDto refreshTokenDto)
        {
            var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
            await _repository.AddAsync(refreshToken);
        }
    }
