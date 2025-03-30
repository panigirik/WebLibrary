using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Persistance;
using WebLibrary.Persistance.Repositories;
using Xunit;

namespace WebLibrary.Infrastructure.Tests.AuthorRepositoryTests
{
    public class UpdateAsyncHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthorRepository _repository;

        public UpdateAsyncHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_Update")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new AuthorRepository(_context);
        }

        [Fact]
        public async Task UpdateAsync_AuthorExists_UpdatesAuthor()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author 
            { 
                AuthorId = authorId, 
                FirstName = "Initial Name", 
                LastName = "LastName", 
                Country = "Initial Country"
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            // Modify author
            author.FirstName = "Updated Name";
            author.Country = "Updated Country";

            // Act
            await _repository.UpdateAsync(author);
            var updatedAuthor = await _context.Authors.FindAsync(authorId);

            // Assert
            Assert.NotNull(updatedAuthor);
            Assert.Equal("Updated Name", updatedAuthor.FirstName);
            Assert.Equal("Updated Country", updatedAuthor.Country);
        }

    }
}
