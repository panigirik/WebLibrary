namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;

public interface IDeleteUserUseCase
{
    Task ExecuteAsync(Guid id);
}