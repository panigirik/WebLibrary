using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Domain.Exceptions;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IBookValidationService _bookValidationService;
    
    public BooksController(IBookService bookService, IBookValidationService bookValidationService)
    {
        _bookService = bookService;
        _bookValidationService = bookValidationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetBookRequestDto>> GetBookById(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            throw new NotFoundException("Book not found.");
        }
        
        return Ok(book);
    }

    [HttpGet("isbn/{isbn}")]
    public async Task<ActionResult<GetBookRequestDto>> GetBookByIsbn(string isbn)
    {
        var book = await _bookService.GetBookByIsbnAsync(isbn);
        if (book == null)
        {
            throw new NotFoundException("Book not found.");
        }
        return Ok(book);
    }

    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetPaginatedBooks([FromQuery] PaginatedBookFilter filter)
    {
        var books = await _bookService.GetPaginatedBooksAsync(filter);
        return Ok(books);
    }

    [HttpGet("author/{authorId}")]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetBooksByAuthor(Guid authorId)
    {
        var books = await _bookService.GetBooksByAuthorAsync(authorId);
        return Ok(books);
    }

    [HttpPost] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AddBook([FromForm] BookDto bookDto)
    {
        if (bookDto.ImageFile != null)
        {
            await _bookValidationService.ValidateBookAsync(bookDto, bookDto.ImageFile);
            if (bookDto.ImageFile != null)
            {
                using var memoryStream = new MemoryStream();
                await bookDto.ImageFile.CopyToAsync(memoryStream);
            }
        }

        await _bookService.AddBookAsync(bookDto);
        return Ok();
    }

    [HttpGet("image/{id}")]
    public async Task<IActionResult> GetBookImage(Guid id)
    {
        var imageData = await _bookService.GetBookImageAsync(id);
        if (imageData == null)
        {
            throw new NotFoundException("Book image not found.");
        }

        return File(imageData, "image/jpeg");
    }

    [HttpPost("{bookId}/borrow")]
    public async Task<IActionResult> BorrowBook(Guid bookId, [FromQuery] Guid userId)
    {
        await _bookService.BorrowBookAsync(bookId, userId);
        return Ok("Book successfully borrowed.");
    }

    [HttpPost("{bookId}/return")]
    public async Task<IActionResult> ReturnBook(Guid bookId, [FromQuery] Guid userId)
    {
        await _bookService.ReturnBookAsync(bookId, userId);
        return Ok("Book successfully returned.");
    }

    [HttpPut] [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> UpdateBook([FromBody] BookDto bookDto)
    {
        await _bookService.UpdateBookAsync(bookDto);
        return NoContent();
    }

    [HttpDelete("{id}")] [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> DeleteBook(Guid id)
    {
        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}
