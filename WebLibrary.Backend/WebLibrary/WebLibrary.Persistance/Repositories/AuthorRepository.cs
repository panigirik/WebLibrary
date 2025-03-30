using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Persistance.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Author.
/// </summary>
public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Конструктор репозитория.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получить автора по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автора.</param>
    /// <returns>Автор.</returns>
    public async Task<Author> GetByIdAsync(Guid id)
    {
        return await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.AuthorId == id);
    }

    /// <summary>
    /// Получить всех авторов.
    /// </summary>
    /// <returns>Список авторов.</returns>
    public IEnumerable<Author> GetAllAsync()
    {
        return _context.Authors.OrderByDescending(a => a.DateOfBirth);
    }

    /// <summary>
    /// Добавить нового автора.
    /// </summary>
    /// <param name="author">Автор.</param>
    public async Task AddAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить информацию об авторе.
    /// </summary>
    /// <param name="author">Обновленный автор.</param>
    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить автора по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автора.</param>
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
