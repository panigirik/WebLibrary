using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IGetAllBooksUseCase
{
    Task<IEnumerable<GetBookRequestDto>> ExecuteAsync();
}