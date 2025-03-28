using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Enums;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Domain.Interfaces;

public interface IBookRepository
{
    Task<Book> GetByIdAsync(Guid id);

    Task<Book> GetByIsbnAsync(string isbn);

    Task<List<Book>> GetPaginated(PaginatedBookFilter filter);

    
    Task<IEnumerable<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId);
    
    Task<IEnumerable<Book>> GetOverdueBooksAsync();


}