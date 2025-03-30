using Microsoft.AspNetCore.Authorization;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Domain.Exceptions;

namespace WebLibrary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

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

    [HttpPost] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorDto authorDto)
    {
        if (authorDto == null)
            throw new BadRequestException("Author data cannot be null.");

        await _authorService.AddAuthorAsync(authorDto);
        return CreatedAtAction(nameof(GetAuthorById), new { id = authorDto.AuthorId }, authorDto);
    }

    [HttpPut("{id:guid}")] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorDto authorDto)
    {
        if (authorDto == null || id != authorDto.AuthorId)
            throw new BadRequestException("Invalid author data.");

        await _authorService.UpdateAuthorAsync(authorDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")] [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        if (id == Guid.Empty)
            throw new BadRequestException("Invalid author ID.");

        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }

    [HttpGet("{authorId:guid}/books")]
    public async Task<IActionResult> GetBooksByAuthor(Guid authorId)
    {
        if (authorId == Guid.Empty)
            throw new BadRequestException("Invalid author ID.");

        var books = await _authorService.GetBooksByAuthorAsync(authorId);
        return Ok(books);
    }
}
