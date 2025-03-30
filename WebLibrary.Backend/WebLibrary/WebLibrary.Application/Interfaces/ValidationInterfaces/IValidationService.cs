using FluentValidation.Results;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

public interface IValidationService
{
    Task<ValidationResult> ValidateLoginRequestAsync(LoginRequest request);

    Task<ValidationResult> ValidateUserAsync(LoginRequest request);
}