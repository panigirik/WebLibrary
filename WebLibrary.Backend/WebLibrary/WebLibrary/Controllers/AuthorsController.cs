using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebLibrary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
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
        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound();

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorDto authorDto)
    {
        if (authorDto == null)
            return BadRequest();

        await _authorService.AddAuthorAsync(authorDto);
        return CreatedAtAction(nameof(GetAuthorById), new { id = authorDto.AuthorId }, authorDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorDto authorDto)
    {
        if (authorDto == null || id != authorDto.AuthorId)
            return BadRequest();

        await _authorService.UpdateAuthorAsync(authorDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return NoContent();
    }

    [HttpGet("{authorId:guid}/books")]
    public async Task<IActionResult> GetBooksByAuthor(Guid authorId)
    {
        var books = await _authorService.GetBooksByAuthorAsync(authorId);
        return Ok(books);
    }
}
