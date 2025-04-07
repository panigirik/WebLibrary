using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.UserUseCases;

    /// <summary>
    /// Use-case для получения пользователя по его идентификатору.
    /// </summary>
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetUserByIdUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Объект для маппинга сущностей в DTO.</param>
        public GetUserByIdUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет получение пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя, по которому производится поиск.</param>
        /// <returns>Пользователь в виде <see cref="UserDto"/>, если найден.</returns>
        public async Task<UserDto?> ExecuteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserDto?>(user);
        }
    }
