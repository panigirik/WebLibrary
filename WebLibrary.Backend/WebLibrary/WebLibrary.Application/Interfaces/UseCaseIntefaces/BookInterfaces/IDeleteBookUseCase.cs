namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IDeleteBookUseCase
{
    Task ExecuteAsync(Guid id);
}