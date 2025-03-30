using FluentValidation;
using FluentValidation.Results;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.Services;

    /// <summary>
    /// Сервис для валидации запросов пользователя.
    /// </summary>
    public class ValidationService : IValidationService
    {
        private readonly IValidator<LoginRequest> _loginRequestValidator;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ValidationService"/>.
        /// </summary>
        /// <param name="loginRequestValidator">Валидатор для запросов логина.</param>
        public ValidationService(IValidator<LoginRequest> loginRequestValidator)
        {
            _loginRequestValidator = loginRequestValidator;
        }

        /// <summary>
        /// Выполняет валидацию запроса на логин.
        /// </summary>
        /// <param name="request">Запрос на логин.</param>
        /// <returns>Результат валидации.</returns>
        public async Task<ValidationResult> ValidateLoginRequestAsync(LoginRequest request)
        {
            return await _loginRequestValidator.ValidateAsync(request);
        }

        /// <summary>
        /// Выполняет валидацию пользователя.
        /// </summary>
        /// <param name="request">Запрос на логин пользователя.</param>
        /// <returns>Результат валидации.</returns>
        public async Task<ValidationResult> ValidateUserAsync(LoginRequest request)
        {
            return await _loginRequestValidator.ValidateAsync(request);
        }
    }
