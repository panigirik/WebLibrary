using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Domain.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
   
    
    public BookService(IBookRepository bookRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
      }

    public async Task<IEnumerable<GetBookRequestDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
    }

    public async Task<GetBookRequestDto?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return _mapper.Map<GetBookRequestDto?>(book);
    }

    public async Task<IEnumerable<GetBookRequestDto>> GetPaginatedBooksAsync(PaginatedBookFilter filter)
    {
        var books = await _bookRepository.GetPaginated(filter);
        return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
    }

    
    public async Task<GetBookRequestDto?> GetBookByIsbnAsync(string isbn)
    {
        var book = await _bookRepository.GetByIsbnAsync(isbn);
        return _mapper.Map<GetBookRequestDto?>(book);
    }

    public async Task<IEnumerable<GetBookRequestDto>> GetBooksByAuthorAsync(Guid authorId)
    {
        var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
        return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
    }

    public async Task AddBookAsync(BookDto bookDto)
    {
        var book = new Book
        {
            BookId = Guid.NewGuid(),
            ISBN = bookDto.ISBN,
            Title = bookDto.Title,
            Genre = bookDto.Genre,
            Description = bookDto.Description,
            AuthorId = bookDto.AuthorId,
            BorrowedAt = bookDto.BorrowedAt,
            ReturnBy = bookDto.ReturnBy,
            BorrowedById = bookDto.BorrowedById,
            IsAvailable = bookDto.IsAvailable
        };

        if (bookDto.ImageFile != null)
        {
            using var memoryStream = new MemoryStream();
            await bookDto.ImageFile.CopyToAsync(memoryStream);
            book.ImageData = memoryStream.ToArray();
        }

        await _bookRepository.AddAsync(book);
    }

    
    public async Task<byte[]?> GetBookImageAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book?.ImageData != null)
        {
            return book.ImageData;
        }

        return null;
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
        book.ReturnBy = DateTime.UtcNow.AddDays(14);

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