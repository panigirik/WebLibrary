using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return _mapper.Map<BookDto?>(book);
    }

    public async Task<IEnumerable<BookDto>> GetPaginatedBooksAsync(PaginatedBookFilter filter)
    {
        var books = await _bookRepository.GetPaginated(filter);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    
    public async Task<BookDto?> GetBookByIsbnAsync(string isbn)
    {
        var book = await _bookRepository.GetByIsbnAsync(isbn);
        return _mapper.Map<BookDto?>(book);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId)
    {
        var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task AddBookAsync(BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        await _bookRepository.AddAsync(book);
    }

    public async Task UpdateBookAsync(BookDto bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await _bookRepository.DeleteAsync(id);
    }
    
    public async Task<bool> BorrowBookAsync(Guid bookId, Guid userId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null || !book.IsAvailable)
            return false;

        book.IsAvailable = false;
        book.BorrowedById = userId;
        book.BorrowedAt = DateTime.UtcNow;
        book.ReturnBy = DateTime.UtcNow.AddDays(14); // Даем 2 недели на возврат

        await _bookRepository.UpdateAsync(book);
        return true;
    }
    
    public async Task<bool> ReturnBookAsync(Guid bookId, Guid userId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null || book.BorrowedById != userId)
            return false;

        book.IsAvailable = true;
        book.BorrowedById = null;
        book.BorrowedAt = DateTime.UtcNow;
        book.ReturnBy = DateTime.UtcNow;

        await _bookRepository.UpdateAsync(book);
        return true;
    }


}