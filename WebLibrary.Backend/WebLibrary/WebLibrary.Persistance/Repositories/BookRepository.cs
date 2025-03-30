using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebLibrary.BackgroundService.Redis;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Enums;
using WebLibrary.Domain.Filters;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Persistance.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;
    private readonly RedisCacheService _redisCacheService;
    private readonly IMapper _mapper;
   
    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.AuthorId == id);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books
            .ToListAsync();
    }
    
    public async Task<List<Book>> GetPaginated(PaginatedBookFilter filter)
    {
        IQueryable<Book> booksQuery = BuildQuery(filter);
        IOrderedQueryable<Book> sortedBooks = SortBooks(booksQuery, filter.TypeOfSort);
        return await GetPaginatedBooksAsync(sortedBooks, filter.PageNumber, filter.PageSize);
    }
    
    private IQueryable<Book> BuildQuery(PaginatedBookFilter filter)
    {
        IQueryable<Book> query = _context.Books.AsQueryable();

        if (filter.PostCategory != Category.All)
        {
            query = query.Where(b => b.Genre == filter.PostCategory.ToString());
        }

        return query;
    }

   
    private async Task<List<Book>> GetPaginatedBooksAsync(IQueryable<Book> query, int pageNumber, int pageSize)
    {
        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    
    private static IOrderedQueryable<Book> SortBooks(IQueryable<Book> books, TypeOfSort sortBy)
    {
        return sortBy switch
        {
            TypeOfSort.Title => books.OrderBy(b => b.Title),
            TypeOfSort.Date => books.OrderByDescending(b => b.BorrowedAt),
            _ => books.OrderBy(b => b.Title)
        };
    }

    public async Task<Book> GetByIsbnAsync(string isbn)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
    }

    public async Task AddAsync(Book book)
    {
        book.BookId = Guid.NewGuid();

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };
        //await _redisCacheService.SetAsync($"book_{book.BookId}", book, cacheOptions);
    }

    public async Task UpdateAsync(Book updatedBook)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == updatedBook.BookId);
        if (book == null) return;
    
        book.Title = updatedBook.Title;
        book.AuthorId = updatedBook.AuthorId;
        book.Genre = updatedBook.Genre;
        book.ISBN = updatedBook.ISBN;
        book.BorrowedAt = updatedBook.BorrowedAt;
        book.ReturnBy = updatedBook.ReturnBy;
        book.BorrowedById = updatedBook.BorrowedById;
        book.IsAvailable = updatedBook.IsAvailable;

        _context.Entry(book).State = EntityState.Modified; // Явное отслеживание изменений
        await _context.SaveChangesAsync();

        await _redisCacheService.RemoveAsync($"book_{book.BookId}"); // Очистка кеша

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };
        await _redisCacheService.SetAsync($"book_{book.BookId}", book, cacheOptions);
    }


    public async Task DeleteAsync(Guid id)
    {
        var book = await GetByIdAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
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
            .Where(b => b.BorrowedAt < DateTime.UtcNow.AddDays(14) && !b.IsAvailable)
            .ToListAsync();
    }

    public async Task RemoveBookFromCache(Guid bookId)
    {
        string cacheKey = $"book_{bookId}";
        await _redisCacheService.RemoveAsync(cacheKey);
    }
    
}