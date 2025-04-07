using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Enums;
using WebLibrary.Domain.Filters;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Persistance.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Book.
/// </summary>
public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;
    
    /// <summary>
    /// Конструктор репозитория.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получить книгу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Книга.</returns>
    public async Task<Book> GetByIdAsync(Guid id)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.AuthorId == id);
    }

    /// <summary>
    /// Получить книгу по ISBN.
    /// </summary>
    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Книга.</returns>
    public async Task<Book> GetByIsbnAsync(string isbn)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
    }

    /// <summary>
    /// Получить все книги.
    /// </summary>
    /// <returns>Список книг.</returns>
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books
            .ToListAsync();
    }

    /// <summary>
    /// Получить книги с пагинацией.
    /// </summary>
    /// <param name="filter">Фильтр пагинации.</param>
    /// <returns>Список книг.</returns>
    public async Task<List<Book>> GetPaginated(PaginatedBookFilter filter)
    {
        IQueryable<Book> booksQuery = BuildQuery(filter);
        IOrderedQueryable<Book> sortedBooks = SortBooks(booksQuery, filter.TypeOfSort);
        return await GetPaginatedBooksAsync(sortedBooks, filter.PageNumber, filter.PageSize);
    }

    /// <summary>
    /// Построить запрос для фильтрации книг.
    /// </summary>
    /// <param name="filter">Фильтр для построения запроса.</param>
    /// <returns>Запрос для получения книг.</returns>
    private IQueryable<Book> BuildQuery(PaginatedBookFilter filter)
    {
        IQueryable<Book> query = _context.Books.AsQueryable();

        if (filter.BookCategory != Category.All)
        {
            query = query.Where(b => b.Genre == filter.BookCategory.ToString());
        }

        return query;
    }

    /// <summary>
    /// Получить книги с пагинацией.
    /// </summary>
    /// <param name="query">Запрос для получения книг.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Список книг с пагинацией.</returns>
    private async Task<List<Book>> GetPaginatedBooksAsync(IQueryable<Book> query, int pageNumber, int pageSize)
    {
        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(Guid authorId)
    {
        return await _context.Books
            .Where(b => b.AuthorId == authorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetOverdueBooksAsync()
    {
        return await _context.Books
            .Where(b => b.ReturnBy.HasValue && b.ReturnBy.Value < DateTime.UtcNow && !b.IsAvailable)
            .ToListAsync();
    }


    /// <summary>
    /// Отсортировать книги по заданному критерию.
    /// </summary>
    /// <param name="books">Запрос для книг.</param>
    /// <param name="sortBy">Тип сортировки.</param>
    /// <returns>Отсортированный запрос для книг.</returns>
    private static IOrderedQueryable<Book> SortBooks(IQueryable<Book> books, TypeOfSort sortBy)
    {
        return sortBy switch
        {
            TypeOfSort.Title => books.OrderBy(b => b.Title),
            TypeOfSort.Date => books.OrderByDescending(b => b.BorrowedAt),
            _ => books.OrderBy(b => b.Title)
        };
    }

    /// <summary>
    /// Добавить книгу.
    /// </summary>
    /// <param name="book">Книга.</param>
    public async Task AddAsync(Book book)
    {
        book.BookId = Guid.NewGuid();

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        
    }

    /// <summary>
    /// Обновить книгу.
    /// </summary>
    /// <param name="updatedBook">Обновленная книга.</param>
    public async Task UpdateAsync(Book updatedBook)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == updatedBook.BookId);
        if (book == null) return;

        book.Title = updatedBook.Title;
        book.AuthorId = updatedBook.AuthorId;

        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить книгу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор книги.</param>
    public async Task DeleteAsync(Guid id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
