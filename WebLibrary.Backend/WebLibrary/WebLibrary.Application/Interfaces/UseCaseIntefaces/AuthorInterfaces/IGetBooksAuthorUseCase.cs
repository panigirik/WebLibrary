using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;

/// <summary>
/// Получение книг автора.
/// </summary>
public interface IGetBooksAuthorUseCase
{
    public Task<IEnumerable<BookDto>> ExecuteAsync(Guid authorId);
}