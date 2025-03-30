using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<GetBookRequestDto>> GetAllBooksAsync();

    Task<GetBookRequestDto?> GetBookByIdAsync(Guid id);

    Task<GetBookRequestDto?> GetBookByIsbnAsync(string isbn);

    Task<IEnumerable<GetBookRequestDto>> GetBooksByAuthorAsync(Guid authorId);

    Task<IEnumerable<GetBookRequestDto>> GetPaginatedBooksAsync(PaginatedBookFilter filter);

    Task AddBookAsync(BookDto bookDto);

    Task UpdateBookAsync(BookDto bookDto);

    Task DeleteBookAsync(Guid id);
    
    Task<bool> BorrowBookAsync(Guid bookId, Guid userId);
    
    Task<bool> ReturnBookAsync(Guid bookId, Guid userId);

    Task<byte[]?> GetBookImageAsync(Guid bookId);


}