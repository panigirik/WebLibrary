using AutoMapper;
using FluentValidation;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Application.Interfaces.UseCases;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.UserUseCases;

    /// <summary>
    /// Use-case для обновления информации о пользователе.
    /// </summary>
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateUserInfoRequest> _updateUserValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UpdateUserUseCase"/>.
        /// </summary>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="updateUserInfoUseCase">Use-case для обновления информации о пользователе.</param>
        /// <param name="mapper">Объект для маппинга данных.</param>
        public UpdateUserUseCase(IUserRepository userRepository, 
            IValidator<UpdateUserInfoRequest> updateUserValidator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _updateUserValidator = updateUserValidator;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет обновление информации о пользователе.
        /// </summary>
        /// <param name="updateUserInfoRequest">Запрос с данными для обновления информации о пользователе.</param>
        /// <exception cref="BadRequestException">Бросается, если имя пользователя пусто.</exception>
        /// <exception cref="NotFoundException">Бросается, если пользователь не найден.</exception>
        /// <exception cref="ValidationException">Бросается, если пользователь с таким email уже существует.</exception>
        public async Task ExecuteAsync(UpdateUserInfoRequest updateUserInfoRequest)
        {
            var user = _mapper.Map<User>(updateUserInfoRequest);
            
            if (string.IsNullOrEmpty(updateUserInfoRequest.UserName))
            {
                throw new BadRequestException("Username cannot be empty");
            }
            
            var byIdUser = await _userRepository.GetByIdAsync(updateUserInfoRequest.UserId);
            if (byIdUser == null)
            {
                throw new NotFoundException("User not found");
            }
            
            var byEmailUser = await _userRepository.GetByEmailAsync(updateUserInfoRequest.Email);
            if (byEmailUser != null)
            {
                throw new ValidationException("User with this email already exists");
            }
            
            await _updateUserValidator.ValidateAsync(updateUserInfoRequest);
            await _userRepository.UpdateAsync(user);
        }
    }

