using FluentValidation;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.ValidationServices.ValidateRules;

namespace WebLibrary.ValidationServices.UseCases;

/// <summary>
/// UseCase валидации при добавлении пользователя.
/// </summary>
public class AddUserValidationService : IAddUserValidationService
{
    private readonly AbstractValidator<UserDto> _validator;
    
    public AddUserValidationService()
    {
        _validator = new UserDtoValidator();
    }

    /// <summary>
    /// Выполняет валидацию при создании пользователя.
    /// </summary>
    public async Task ValidateAsync(UserDto userDto)
    {
        var validationResult = await _validator.ValidateAsync(userDto);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("bad request");
        }
    }
}