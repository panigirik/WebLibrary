using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.RefreshTokenUseCases;

    /// <summary>
    /// Use-case для отзыва refresh-токена.
    /// </summary>
    public class RevokeRefreshTokenUseCase : IRevokeRefreshTokenUseCase
    {
        private readonly IRefreshTokenRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RevokeRefreshTokenUseCase"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с refresh-токенами.</param>
        public RevokeRefreshTokenUseCase(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Отзывает refresh-токен по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор refresh-токена.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task ExecuteAsync(Guid id)
        {
            var token = await _repository.GetByIdAsync(id);
            if (token != null)
            {
                token.IsRevoked = true;
                await _repository.UpdateAsync(token);
            }
        }
    }
