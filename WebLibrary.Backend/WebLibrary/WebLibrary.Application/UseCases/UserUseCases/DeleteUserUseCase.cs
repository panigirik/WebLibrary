using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.UserUseCases;

    /// <summary>
    /// Use-case для удаления пользователя.
    /// </summary>
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteUserUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        public DeleteUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Выполняет удаление пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя, которого необходимо удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="NotFoundException">Бросает исключение, если пользователь с таким идентификатором не найден.</exception>
        public async Task ExecuteAsync(Guid id)
        {
            // Проверка существования пользователя с данным id
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }

            // Удаление пользователя из репозитория
            await _userRepository.DeleteAsync(id);
        }
    }
