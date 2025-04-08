using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.UserUseCases;

    /// <summary>
    /// Use-case для удаления пользователя.
    /// </summary>
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteUserUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Репозиторий для работы с пользователями.</param>
        public DeleteUserUseCase(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет удаление пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя, которого необходимо удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="NotFoundException">Бросает исключение, если пользователь с таким идентификатором не найден.</exception>
        public async Task ExecuteAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var existingUser = await _userRepository.GetByIdAsync(user.UserId);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }
            await _userRepository.DeleteAsync(user);
        }
    }
