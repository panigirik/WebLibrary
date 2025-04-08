using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IDeleteBookUseCase
{
    Task ExecuteAsync(BookDto bookDto);
}