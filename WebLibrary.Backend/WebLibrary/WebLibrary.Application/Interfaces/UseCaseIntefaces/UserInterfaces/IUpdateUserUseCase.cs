using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;

public interface IUpdateUserUseCase
{
    Task ExecuteAsync(UpdateUserInfoRequest updateUserInfoRequest);
}