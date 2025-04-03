using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IGetBooksByAuthorUseCase
{
    Task<IEnumerable<GetBookRequestDto>> ExecuteAsync(Guid authorId);
}