using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Persistance;
using WebLibrary.Persistance.Repositories;
using Xunit;

namespace WebLibrary.Infrastructure.Tests.AuthorRepositoryTests;

public class GetByIdAsyncHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly AuthorRepository _repository;

    public GetByIdAsyncHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new AuthorRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_AuthorExists_ReturnsAuthorWithBooks()
    {
        var authorId = Guid.NewGuid();
        var author = new Author 
        { 
            AuthorId = authorId, 
            FirstName = "Test Author", 
            LastName = "TestLastName", 
            Country = "TestCountry"    
        };
    
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        
        var result = await _repository.GetByIdAsync(authorId);
        
        Assert.NotNull(result);
        Assert.Equal(authorId, result.AuthorId);
        Assert.Equal("Test Author", result.FirstName);
        Assert.Equal("TestLastName", result.LastName);  
        Assert.Equal("TestCountry", result.Country);   
    }


    [Fact]
    public async Task GetByIdAsync_AuthorDoesNotExist_ReturnsNull()
    {
        var result = await _repository.GetByIdAsync(Guid.NewGuid());
        
        Assert.Null(result);
    }
}