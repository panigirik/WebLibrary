using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.UserUseCases;

    /// <summary>
    /// Use-case для получения пользователя по его email.
    /// </summary>
    public class GetUserByEmailUseCase : IGetUserByEmailUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetUserByEmailUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Объект для маппинга сущностей в DTO.</param>
        public GetUserByEmailUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет получение пользователя по email.
        /// </summary>
        /// <param name="email">Email пользователя, по которому производится поиск.</param>
        /// <returns>Пользователь в виде <see cref="UserDto"/>, если найден.</returns>
        /// <exception cref="NotFoundException">Выбрасывается, если пользователь не найден.</exception>
        public async Task<UserDto?> ExecuteAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return _mapper.Map<UserDto?>(user);
        }
    }
