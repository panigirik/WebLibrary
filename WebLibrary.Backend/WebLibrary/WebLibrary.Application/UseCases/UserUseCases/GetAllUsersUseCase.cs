using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.UserUseCases;

    /// <summary>
    /// Use-case для получения всех пользователей.
    /// </summary>
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetAllUsersUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Объект для маппинга сущностей в DTO.</param>
        public GetAllUsersUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет получение всех пользователей.
        /// </summary>
        /// <returns>Список пользователей в виде <see cref="UserDto"/>.</returns>
        public async Task<IEnumerable<UserDto>> ExecuteAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
