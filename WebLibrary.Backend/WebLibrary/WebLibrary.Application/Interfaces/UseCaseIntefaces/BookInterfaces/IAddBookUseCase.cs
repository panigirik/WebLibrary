using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IAddBookUseCase
{
    Task ExecuteAsync(AddBookRequest bookRequest);
}
