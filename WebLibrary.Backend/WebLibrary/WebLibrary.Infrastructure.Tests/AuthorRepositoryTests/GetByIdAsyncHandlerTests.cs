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
        // Arrange
        var authorId = Guid.NewGuid();
        var author = new Author 
        { 
            AuthorId = authorId, 
            FirstName = "Test Author", 
            LastName = "TestLastName",  // <-- Добавлено
            Country = "TestCountry"     // <-- Добавлено
        };
    
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(authorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authorId, result.AuthorId);
        Assert.Equal("Test Author", result.FirstName);
        Assert.Equal("TestLastName", result.LastName);  // <-- Проверяем LastName
        Assert.Equal("TestCountry", result.Country);    // <-- Проверяем Country
    }


    [Fact]
    public async Task GetByIdAsync_AuthorDoesNotExist_ReturnsNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}