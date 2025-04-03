namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IGetBookImageUseCase
{
    Task<byte[]?> ExecuteAsync(Guid bookId);
}