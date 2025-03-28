using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();

    Task<BookDto?> GetBookByIdAsync(Guid id);

    Task<BookDto?> GetBookByIsbnAsync(string isbn);

    Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId);

    Task<IEnumerable<BookDto>> GetPaginatedBooksAsync(PaginatedBookFilter filter);

    Task AddBookAsync(BookDto bookDto);

    Task UpdateBookAsync(BookDto bookDto);

    Task DeleteBookAsync(Guid id);
    
    Task<bool> BorrowBookAsync(Guid bookId, Guid userId);
    
    Task<bool> ReturnBookAsync(Guid bookId, Guid userId);
}