using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Domain.Exceptions;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для работы с книгами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IBookValidationService _bookValidationService;
    
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="BooksController"/>.
    /// </summary>
    /// <param name="bookService">Сервис для работы с книгами.</param>
    /// <param name="bookValidationService">Сервис для валидации данных книг.</param>
    public BooksController(IBookService bookService, IBookValidationService bookValidationService)
    {
        _bookService = bookService;
        _bookValidationService = bookValidationService;
    }

    /// <summary>
    /// Получает список всех книг.
    /// </summary>
    /// <returns>Список всех книг.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    /// <summary>
    /// Получает книгу по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Книга с указанным идентификатором.</returns>
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

    /// <summary>
    /// Получает книгу по её ISBN.
    /// </summary>
    /// <param name="isbn">ISBN книги.</param>
    /// <returns>Книга с указанным ISBN.</returns>
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

    /// <summary>
    /// Получает книги с пагинацией.
    /// </summary>
    /// <param name="filter">Фильтр для пагинации.</param>
    /// <returns>Список книг с пагинацией.</returns>
    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetPaginatedBooks([FromQuery] PaginatedBookFilter filter)
    {
        var books = await _bookService.GetPaginatedBooksAsync(filter);
        return Ok(books);
    }

    /// <summary>
    /// Получает книги автора по идентификатору.
    /// </summary>
    /// <param name="authorId">Идентификатор автора.</param>
    /// <returns>Список книг автора.</returns>
    [HttpGet("author/{authorId}")]
    public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetBooksByAuthor(Guid authorId)
    {
        var books = await _bookService.GetBooksByAuthorAsync(authorId);
        return Ok(books);
    }

    /// <summary>
    /// Добавляет новую книгу.
    /// </summary>
    /// <param name="bookDto">Данные книги для добавления.</param>
    /// <returns>Статус выполнения операции.</returns>
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

    /// <summary>
    /// Получает изображение книги по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Изображение книги.</returns>
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

    /// <summary>
    /// Оформляет книгу как заёмную для пользователя.
    /// </summary>
    /// <param name="bookId">Идентификатор книги.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Статус выполнения операции.</returns>
    [HttpPost("{bookId}/borrow")]
    public async Task<IActionResult> BorrowBook(Guid bookId, [FromQuery] Guid userId)
    {
        await _bookService.BorrowBookAsync(bookId, userId);
        return Ok("Book successfully borrowed.");
    }

    /// <summary>
    /// Возвращает книгу пользователем.
    /// </summary>
    /// <param name="bookId">Идентификатор книги.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Статус выполнения операции.</returns>
    [HttpPost("{bookId}/return")]
    public async Task<IActionResult> ReturnBook(Guid bookId, [FromQuery] Guid userId)
    {
        await _bookService.ReturnBookAsync(bookId, userId);
        return Ok("Book successfully returned.");
    }

    /// <summary>
    /// Обновляет информацию о книге.
    /// </summary>
    /// <param name="bookDto">Данные книги для обновления.</param>
    /// <returns>Статус выполнения операции.</returns>
    [HttpPut] [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> UpdateBook([FromBody] BookDto bookDto)
    {
        await _bookService.UpdateBookAsync(bookDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет книгу по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор книги.</param>
    /// <returns>Статус выполнения операции.</returns>
    [HttpDelete("{id}")] [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> DeleteBook(Guid id)
    {
        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}
