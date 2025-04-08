using FluentValidation;
using WebLibrary.Application.Requests;

namespace WebLibrary.Application.ValidateRules;

/// <summary>
/// Валидатор для обновления данных о пользователе.
/// </summary>
public class UpdateUserInfoValidator: AbstractValidator<UpdateUserInfoRequest>
{
    public UpdateUserInfoValidator()
    {
        RuleFor(p => p.UserName).NotEmpty().WithMessage("UserName cannot be empty");
        RuleFor(p => p.Email).NotEmpty().Matches("[A-Z]")
            .Matches("[a-z]").WithMessage("email must container uppercase letters");

    }
}