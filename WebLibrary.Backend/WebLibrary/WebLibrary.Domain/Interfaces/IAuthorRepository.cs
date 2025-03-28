using WebLibrary.Domain.Entities;

namespace WebLibrary.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author> GetByIdAsync(Guid id);
    IEnumerable<Author> GetAllAsync();
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Guid id);
}