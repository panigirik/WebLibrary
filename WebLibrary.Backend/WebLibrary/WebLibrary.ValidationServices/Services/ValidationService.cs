using FluentValidation;
using FluentValidation.Results;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.Services;

public class ValidationService : IValidationService
{
    private readonly IValidator<LoginRequest> _loginRequestValidator;

    public ValidationService(IValidator<LoginRequest> loginRequestValidator)
    {
        _loginRequestValidator = loginRequestValidator;
    }

    public async Task<ValidationResult> ValidateLoginRequestAsync(LoginRequest request)
    {
        return await _loginRequestValidator.ValidateAsync(request);
    }
    
    public async Task<ValidationResult> ValidateUserAsync(LoginRequest request)
    {
        return await _loginRequestValidator.ValidateAsync(request);
        
    }

}