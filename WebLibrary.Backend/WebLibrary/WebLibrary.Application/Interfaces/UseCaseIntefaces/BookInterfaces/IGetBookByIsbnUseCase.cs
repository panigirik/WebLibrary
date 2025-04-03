using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IGetBookByIsbnUseCase
{
    Task<GetBookRequestDto?> ExecuteAsync(string isbn);
}