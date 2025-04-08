using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.RefreshTokenUseCases;

    /// <summary>
    /// Use-case для удаления refresh-токена.
    /// </summary>
    public class DeleteRefreshTokenUseCase : IDeleteRefreshTokenUseCase
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteRefreshTokenUseCase"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с refresh-токенами.</param>
        public DeleteRefreshTokenUseCase(IRefreshTokenRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Удаляет refresh-токен по его идентификатору.
        /// </summary>
        /// <param name="refreshTokenDto">refresh-токен, который нужно удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления.</returns>
        public async Task ExecuteAsync(RefreshTokenDto refreshTokenDto)
        {
            var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
            await _repository.DeleteAsync(refreshToken);
        }
    }
