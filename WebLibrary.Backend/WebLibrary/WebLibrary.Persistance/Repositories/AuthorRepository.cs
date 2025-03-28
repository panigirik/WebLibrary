using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Persistance.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Author> GetByIdAsync(Guid id)
    {
        return await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.AuthorId == id);
    }

    public  IEnumerable<Author> GetAllAsync()
    {
        return _context.Authors.OrderByDescending(a => a.DateOfBirth);
    }

    public async Task AddAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var author = await GetByIdAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}