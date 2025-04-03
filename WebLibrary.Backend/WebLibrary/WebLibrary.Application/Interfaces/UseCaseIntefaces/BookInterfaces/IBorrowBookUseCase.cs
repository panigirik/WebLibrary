namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IBorrowBookUseCase
{
    Task<bool> ExecuteAsync(Guid bookId, Guid userId);
}
