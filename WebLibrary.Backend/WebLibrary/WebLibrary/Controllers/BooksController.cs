using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.Application.UseCases;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для работы с книгами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IGetAllBooksUseCase _getAllBooksUseCase;
    private readonly IGetBookByIdUseCase _getBookByIdUseCase;
    private readonly IGetBookByIsbnUseCase _getBookByIsbnUseCase;
    private readonly IGetPaginatedBooksUseCase _getPaginatedBooksUseCase;
    private readonly IGetBooksByAuthorUseCase _getBooksByAuthorUseCase;
    private readonly IAddBookUseCase _addBookUseCase;
    private readonly IGetBookImageUseCase _getBookImageUseCase;
    private readonly IBorrowBookUseCase _borrowBookUseCase;
    private readonly IReturnBookUseCase _returnBookUseCase;
    private readonly IUpdateBookUseCase _updateBookUseCase;
    private readonly IDeleteBookUseCase _deleteBookUseCase;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="BooksController"/>.
    /// </summary>
    public BooksController(
        IGetAllBooksUseCase getAllBooksUseCase,
        IGetBookByIdUseCase getBookByIdUseCase,
        IGetBookByIsbnUseCase getBookByIsbnUseCase,
        IGetPaginatedBooksUseCase getPaginatedBooksUseCase,
        IGetBooksByAuthorUseCase getBooksByAuthorUseCase,
        IAddBookUseCase addBookUseCase,
        IGetBookImageUseCase getBookImageUseCase,
        IBorrowBookUseCase borrowBookUseCase,
        IReturnBookUseCase returnBookUseCase,
        IUpdateBookUseCase updateBookUseCase,
        IDeleteBookUseCase deleteBookUseCase)
    {
        _getAllBooksUseCase = getAllBooksUseCase;
        _getBookByIdUseCase = getBookByIdUseCase;
        _getBookByIsbnUseCase = getBookByIsbnUseCase;
        _getPaginatedBooksUseCase = getPaginatedBooksUseCase;
        _getBooksByAuthorUseCase = getBooksByAuthorUseCase;
        _addBookUseCase = addBookUseCase;
        _getBookImageUseCase = getBookImageUseCase;
        _borrowBookUseCase = borrowBookUseCase;
        _returnBookUseCase = returnBookUseCase;
        _updateBookUseCase = updateBookUseCase;
        _deleteBookUseCase = deleteBookUseCase;
    }

    /// <summary>
    /// Получает список всех книг.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetAllBooks()
    {
        var books = await _getAllBooksUseCase.ExecuteAsync();
        return Ok(books);
    }

    /// <summary>
    /// Получает книгу по её идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<GetBookRequestDto>> GetBookById(Guid id)
    {
        var book = await _getBookByIdUseCase.ExecuteAsync(id);
        return Ok(book);
    }

    /// <summary>
    /// Получает книгу по её ISBN.
    /// </summary>
    [HttpGet("isbn/{isbn}")]
    public async Task<ActionResult<GetBookRequestDto>> GetBookByIsbn(string isbn)
    {
        var book = await _getBookByIsbnUseCase.ExecuteAsync(isbn);
        return Ok(book);
    }

    /// <summary>
    /// Получает книги с пагинацией.
    /// </summary>
    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetPaginatedBooks([FromQuery] PaginatedBookFilter filter)
    {
        var books = await _getPaginatedBooksUseCase.ExecuteAsync(filter);
        return Ok(books);
    }

    /// <summary>
    /// Получает книги автора по идентификатору.
    /// </summary>
    [HttpGet("author/{authorId}")]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetBooksByAuthor(Guid authorId)
    {
        var books = await _getBooksByAuthorUseCase.ExecuteAsync(authorId);
        return Ok(books);
    }

    /// <summary>
    /// Добавляет новую книгу.
    /// </summary>
    [HttpPost] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AddBook([FromForm] AddBookRequest bookRequest)
    {
        await _addBookUseCase.ExecuteAsync(bookRequest);
        return Ok();
    }

    /// <summary>
    /// Получает изображение книги по её идентификатору.
    /// </summary>
    [HttpGet("image/{id}")]
    public async Task<IActionResult> GetBookImage(Guid id)
    {
        var imageData = await _getBookImageUseCase.ExecuteAsync(id);
        return File(imageData, "image/jpeg");
    }

    /// <summary>
    /// Оформляет книгу как заёмную для пользователя.
    /// </summary>
    [HttpPost("{bookId}/borrow")]
    public async Task<IActionResult> BorrowBook(Guid bookId, [FromQuery] Guid userId)
    {
        await _borrowBookUseCase.ExecuteAsync(bookId, userId);
        return Ok("Book successfully borrowed.");
    }

    /// <summary>
    /// Возвращает книгу пользователем.
    /// </summary>
    [HttpPost("{bookId}/return")]
    public async Task<IActionResult> ReturnBook(Guid bookId, [FromQuery] Guid userId)
    {
        await _returnBookUseCase.ExecuteAsync(bookId, userId);
        return Ok("Book successfully returned.");
    }

    /// <summary>
    /// Обновляет информацию о книге.
    /// </summary>
    [HttpPut] [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> UpdateBook([FromBody] UpdateBookRequest updateBookRequest)
    {
        await _updateBookUseCase.ExecuteAsync(updateBookRequest);
        return NoContent();
    }

    /// <summary>
    /// Удаляет книгу по её идентификатору.
    /// </summary>
    [HttpDelete("{id}")] [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> DeleteBook(BookDto bookDto)
    {
        await _deleteBookUseCase.ExecuteAsync(bookDto);
        return NoContent();
    }
}
