using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;

public interface IAddUserUseCase
{
    Task ExecuteAsync(UserDto userDto);
}