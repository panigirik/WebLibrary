﻿using Microsoft.AspNetCore.Authorization;
using WebLibrary.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для работы с авторами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthorsController : ControllerBase
{
    private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;
    private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
    private readonly IAddAuthorUseCase _addAuthorUseCase;
    private readonly IUpdateAuthorUseCase _updateAuthorUseCase;
    private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
    private readonly IGetBooksAuthorUseCase _getBooksByAuthorUseCase;
    private readonly ILogger<AuthController> _logger;

    public AuthorsController(
        IGetAllAuthorsUseCase getAllAuthorsUseCase,
        IGetAuthorByIdUseCase getAuthorByIdUseCase,
        IAddAuthorUseCase addAuthorUseCase,
        IUpdateAuthorUseCase updateAuthorUseCase,
        IDeleteAuthorUseCase deleteAuthorUseCase,
        IGetBooksAuthorUseCase getBooksByAuthorUseCase,
        ILogger<AuthController> logger)
    {
        _getAllAuthorsUseCase = getAllAuthorsUseCase;
        _getAuthorByIdUseCase = getAuthorByIdUseCase;
        _addAuthorUseCase = addAuthorUseCase;
        _updateAuthorUseCase = updateAuthorUseCase;
        _deleteAuthorUseCase = deleteAuthorUseCase;
        _getBooksByAuthorUseCase = getBooksByAuthorUseCase;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _getAllAuthorsUseCase.ExecuteAsync();
        return Ok(authors);
    }
    

    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var author = await _getAuthorByIdUseCase.ExecuteAsync(id);
        return Ok(author);
    }

    [HttpPost] 
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> AddAuthor([FromBody] AuthorDto authorDto)
    {
        await _addAuthorUseCase.ExecuteAsync(authorDto);
        return CreatedAtAction(nameof(GetAuthorById), new { id = authorDto.AuthorId }, authorDto);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateAuthor([FromForm] AuthorDto authorDto)
    {
        await _updateAuthorUseCase.ExecuteAsync(authorDto);
        return NoContent();
    }


    [HttpDelete("{id:guid}")] 
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteAuthor(AuthorDto authorDto)
    {
        await _deleteAuthorUseCase.ExecuteAsync(authorDto);
        return NoContent();
    }

    [HttpGet("{authorId:guid}/books")]
    public async Task<IActionResult> GetBooksByAuthor(Guid authorId)
    {
        var books = await _getBooksByAuthorUseCase.ExecuteAsync(authorId);
        return Ok(books);
    }
}
