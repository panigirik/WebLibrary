using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;

public interface IDeleteUserUseCase
{
    Task ExecuteAsync(UserDto userDto);
}