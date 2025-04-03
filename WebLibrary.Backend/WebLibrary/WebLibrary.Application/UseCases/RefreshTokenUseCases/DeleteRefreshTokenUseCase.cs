using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.RefreshTokenUseCases;

    /// <summary>
    /// Use-case для удаления refresh-токена.
    /// </summary>
    public class DeleteRefreshTokenUseCase : IDeleteRefreshTokenUseCase
    {
        private readonly IRefreshTokenRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteRefreshTokenUseCase"/>.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с refresh-токенами.</param>
        public DeleteRefreshTokenUseCase(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Удаляет refresh-токен по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор refresh-токена, который нужно удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления.</returns>
        public async Task ExecuteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
