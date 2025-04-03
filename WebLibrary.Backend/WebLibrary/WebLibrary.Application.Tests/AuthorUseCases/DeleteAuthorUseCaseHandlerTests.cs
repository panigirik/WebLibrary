using Moq;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorUseCases;

    public class DeleteAuthorUseCaseHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly DeleteAuthorUseCase _deleteAuthorUseCase;

        public DeleteAuthorUseCaseHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _deleteAuthorUseCase = new DeleteAuthorUseCase(_authorRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteAuthor_WhenAuthorExists()
        {
            var authorId = Guid.NewGuid();
            var existingAuthor = new Author { AuthorId = authorId, FirstName = "John", LastName = "Doe" };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId)).ReturnsAsync(existingAuthor);

            await _deleteAuthorUseCase.ExecuteAsync(authorId);

            _authorRepositoryMock.Verify(repo => repo.DeleteAsync(authorId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowNotFoundException_WhenAuthorDoesNotExist()
        {
            var authorId = Guid.NewGuid();
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId)).ReturnsAsync((Author)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _deleteAuthorUseCase.ExecuteAsync(authorId));
            Assert.Equal("Author with this id not found", exception.Message);
        }
    }
