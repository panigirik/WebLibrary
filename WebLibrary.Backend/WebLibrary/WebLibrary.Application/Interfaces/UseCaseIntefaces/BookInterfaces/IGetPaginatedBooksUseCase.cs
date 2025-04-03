using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IGetPaginatedBooksUseCase
{
    Task<IEnumerable<GetBookRequestDto>> ExecuteAsync(PaginatedBookFilter filter);
}