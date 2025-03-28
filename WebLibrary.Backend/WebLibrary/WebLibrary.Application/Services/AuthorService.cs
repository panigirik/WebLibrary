using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository authorRepository,
        IBookRepository bookRepository,
        IMapper mapper)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors =  _authorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        return _mapper.Map<AuthorDto?>(author);
    }

    public async Task AddAuthorAsync(AuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        await _authorRepository.AddAsync(author);
    }

    public async Task UpdateAuthorAsync(AuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        await _authorRepository.UpdateAsync(author);
    }

    public async Task DeleteAuthorAsync(Guid id)
    {
        await _authorRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId)
    {
        var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }
}