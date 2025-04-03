using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<UserDto>> ExecuteAsync();
}