using FluentValidation;
using FluentValidation.Results;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.UseCases;

/// <summary>
/// Сервис для выполнения use-case валидации запросов и пользователей.
/// </summary>
public class LoginValidationService : ILoginValidationService
{
    private readonly IValidator<LoginRequest> _loginRequestValidator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidationUseCase"/>.
    /// </summary>
    /// <param name="loginRequestValidator">Валидатор для запросов логина.</param>
    public LoginValidationService(IValidator<LoginRequest> loginRequestValidator)
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
    /// Выполняет валидацию данных пользователя.
    /// </summary>
    /// <param name="request">Запрос на логин пользователя.</param>
    /// <returns>Результат валидации.</returns>
    public async Task<ValidationResult> ValidateUserAsync(LoginRequest request)
    {
        return await _loginRequestValidator.ValidateAsync(request);
    }
}