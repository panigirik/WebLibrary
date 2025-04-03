using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IUpdateBookUseCase
{
    Task ExecuteAsync(UpdateBookRequest updateBookRequest);
}