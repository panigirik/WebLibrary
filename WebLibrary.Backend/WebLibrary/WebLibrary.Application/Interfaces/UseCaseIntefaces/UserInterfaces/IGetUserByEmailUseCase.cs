using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;

public interface IGetUserByEmailUseCase
{
    Task<UserDto?> ExecuteAsync(string email);
}