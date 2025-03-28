using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto?> GetAuthorByIdAsync(Guid id);
    Task AddAuthorAsync(AuthorDto author);
    Task UpdateAuthorAsync(AuthorDto author);
    Task DeleteAuthorAsync(Guid id);
    Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId);
}