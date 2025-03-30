using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Persistance;
using WebLibrary.Persistance.Repositories;
using Xunit;

namespace WebLibrary.Infrastructure.Tests.AuthorRepositoryTests;

public class GetAllAsyncHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly AuthorRepository _repository;

    public GetAllAsyncHandlerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_GetAll")
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new AuthorRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_AuthorsExist_ReturnsAllAuthorsSortedByDateOfBirth()
    {
        // Arrange
        _context.Authors.AddRange(new List<Author>
        {
            new Author { AuthorId = Guid.NewGuid(), FirstName = "Author1", LastName = "Last1", Country = "Country1", DateOfBirth = new DateTime(1980, 1, 1) },
            new Author { AuthorId = Guid.NewGuid(), FirstName = "Author2", LastName = "Last2", Country = "Country2", DateOfBirth = new DateTime(1990, 5, 10) },
            new Author { AuthorId = Guid.NewGuid(), FirstName = "Author3", LastName = "Last3", Country = "Country3", DateOfBirth = new DateTime(1975, 3, 20) }
        });
        await _context.SaveChangesAsync();

        // Act
        var result = _repository.GetAllAsync().ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.True(result[0].DateOfBirth > result[1].DateOfBirth);
        Assert.True(result[1].DateOfBirth > result[2].DateOfBirth);
    }

    [Fact]
    public void GetAllAsync_NoAuthors_ReturnsEmptyList()
    {
        // Act
        var result = _repository.GetAllAsync().ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
