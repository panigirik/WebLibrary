using Microsoft.AspNetCore.Authorization;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Exceptions;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для работы с авторами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AuthorsController"/>.
    /// </summary>
    /// <param name="authorService">Сервис для работы с авторами.</param>
    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
    }

    /// <summary>
    /// Получает список всех авторов.
    /// </summary>
    /// <returns>Список авторов.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    /// <summary>
    /// Получает автора по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автора.</param>
    /// <returns>Автор с указанным идентификатором.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        if (id == Guid.Empty)
            throw new BadRequestException("Invalid author ID.");

        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null)
            throw new NotFoundException("Author not found.");

        return Ok(author);
    }

    /// <summary>
    /// Добавляет нового автора.
    /// </summary>
    /// <param name="authorDto">Данные автора для добавления.</param>
    /// <returns>Статус выполнения операции.</returns>
    [HttpPost] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorDto authorDto)
    {
        if (authorDto == null)
            throw new BadRequestException("Author data cannot be null.");

        await _authorService.AddAuthorAsync(authorDto);
        return CreatedAtAction(nameof(GetAuthorById), new { id = authorDto.AuthorId }, authorDto);
    }

    /// <summary>
    /// Обновляет информацию об авторе.
    /// </summary>
    /// <param name="id">Идентификатор автора.</param>
    /// <param name="authorDto">Данные для обновления.</param>
    /// <returns>Статус выполнения операции.</returns>
    [HttpPut("{id:guid}")] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorDto authorDto)
    {
        if (authorDto == null || id != authorDto.AuthorId)
            throw new BadRequestException("Invalid author data.");

        await _authorService.UpdateAuthorAsync(authorDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет автора.
    /// </summary>
    /// <param name="id">Идентификатор автора.</param>
    /// <returns>Статус выполнения операции.</returns>
    [HttpDelete("{id:guid}")] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        if (id == Guid.Empty)
            throw new BadRequestException("Invalid author ID.");

        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Получает список книг автора.
    /// </summary>
    /// <param name="authorId">Идентификатор автора.</param>
    /// <returns>Список книг автора.</returns>
    [HttpGet("{authorId:guid}/books")]
    public async Task<IActionResult> GetBooksByAuthor(Guid authorId)
    {
        if (authorId == Guid.Empty)
            throw new BadRequestException("Invalid author ID.");

        var books = await _authorService.GetBooksByAuthorAsync(authorId);
        return Ok(books);
    }
}
