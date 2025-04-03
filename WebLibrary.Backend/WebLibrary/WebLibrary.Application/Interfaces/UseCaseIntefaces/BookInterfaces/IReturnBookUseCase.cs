namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IReturnBookUseCase
{
    Task<bool> ExecuteAsync(Guid bookId, Guid userId);
}