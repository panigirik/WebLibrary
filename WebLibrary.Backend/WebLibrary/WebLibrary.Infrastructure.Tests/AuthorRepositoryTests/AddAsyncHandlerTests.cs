using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Persistance;
using WebLibrary.Persistance.Repositories;
using Xunit;

namespace WebLibrary.Infrastructure.Tests.AuthorRepositoryTests;

public class AddAsyncHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly AuthorRepository _repository;

    public AddAsyncHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new AuthorRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ValidAuthor_SavesToDatabase()
    {
        // Arrange
        var author = new Author
        {
            AuthorId = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Country = "USA",
            DateOfBirth = new DateTime(1980, 5, 20)
        };

        // Act
        await _repository.AddAsync(author);
        var result = await _context.Authors.FindAsync(author.AuthorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(author.AuthorId, result.AuthorId);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal("USA", result.Country);
    }
}