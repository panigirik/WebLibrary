using AutoMapper;
using FluentValidation;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserValidationService _userValidationService;

        /// <summary>
        /// Конструктор для инициализации сервиса с репозиторием пользователей и маппером.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        /// <param name="mapper">Маппер для преобразования данных.</param>
        public UserService(IUserRepository userRepository, IMapper mapper, IUserValidationService userValidationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userValidationService = userValidationService;
        }

        /// <summary>
        /// Получает всех пользователей.
        /// </summary>
        /// <returns>Список пользователей в виде DTO.</returns>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        /// <summary>
        /// Получает пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователь в виде DTO.</returns>
        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto?>(user);
        }

        /// <summary>
        /// Получает пользователя по адресу электронной почты.
        /// </summary>
        /// <param name="email">Адрес электронной почты пользователя.</param>
        /// <returns>Пользователь в виде DTO.</returns>
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }
            return _mapper.Map<UserDto?>(user);
        }

        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        /// <param name="userDto">DTO нового пользователя.</param>
        public async Task AddUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new ValidationException("user with this email is already exists");
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash, workFactor: 12);
            await _userRepository.AddAsync(user);
        }

        /// <summary>
        /// Обновляет информацию о пользователе.
        /// </summary>
        /// <param name="updateUserInfoRequest">DTO с обновленной информацией о пользователе.</param>
        public async Task UpdateUserAsync(UpdateUserInfoRequest updateUserInfoRequest)
        {
            var user = _mapper.Map<User>(updateUserInfoRequest);
            if (updateUserInfoRequest.UserName == "")
            {
                throw new BadRequestException("username cannot be empty");
            }
            var byIdUser = await _userRepository.GetByIdAsync(updateUserInfoRequest.UserId);
            if (byIdUser == null)
            {
                throw new NotFoundException("user not found");
            }
            var byEmailuser = await _userRepository.GetByEmailAsync(updateUserInfoRequest.Email);
            if (byEmailuser != null)
            {
                throw new ValidationException("user with this email is already exists");
            }
            await _userValidationService.ValidateBookAsync(updateUserInfoRequest);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Удаляет пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя для удаления.</param>
        public async Task DeleteUserAsync(Guid id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException("user not found");
            }
            await _userRepository.DeleteAsync(id);
        }
    }
}
