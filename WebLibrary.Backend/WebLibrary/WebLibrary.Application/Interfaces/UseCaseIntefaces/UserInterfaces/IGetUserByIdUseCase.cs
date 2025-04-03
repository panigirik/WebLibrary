using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;

public interface IGetUserByIdUseCase
{
    Task<UserDto?> ExecuteAsync(Guid id);
}