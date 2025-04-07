using FluentValidation;
using WebLibrary.Application.Dtos;

namespace WebLibrary.ValidationServices.ValidateRules;

/// <summary>
/// Валидатор для обновления данных о пользователе.
/// </summary>
public class UserDtoValidator: AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(p => p.UserName).NotEmpty().WithMessage("UserName cannot be empty");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
    }
}