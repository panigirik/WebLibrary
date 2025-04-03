using AutoMapper;
using FluentValidation;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.UserUseCases;

    /// <summary>
    /// Use-case для добавления нового пользователя.
    /// </summary>
    public class AddUserUseCase : IAddUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddUserUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Объект для маппинга между сущностями и DTO.</param>
        public AddUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет добавление нового пользователя в систему.
        /// </summary>
        /// <param name="userDto">DTO пользователя, содержащий данные для создания нового пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        /// <exception cref="ValidationException">Бросает исключение, если пользователь с таким email уже существует.</exception>
        public async Task ExecuteAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new ValidationException("User with this email already exists");
            }

            var user = _mapper.Map<User>(userDto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash, workFactor: 12);

           await _userRepository.AddAsync(user);
        }
    }
