using Moq;
using WebLibrary.Application.Services;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorServiceTests
{
    public class DeleteAuthorAsyncHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly AuthorService _authorService;

        public DeleteAuthorAsyncHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();

            _authorService = new AuthorService(
                _authorRepositoryMock.Object,
                Mock.Of<IBookRepository>(), // Не нужен для этого метода
                Mock.Of<AutoMapper.IMapper>());
        }

        [Fact]
        public async Task DeleteAuthorAsync_ValidId_CallsRepositoryOnce()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            _authorRepositoryMock.Setup(repo => repo.DeleteAsync(authorId)).Returns(Task.CompletedTask);

            // Act
            await _authorService.DeleteAuthorAsync(authorId);

            // Assert
            _authorRepositoryMock.Verify(repo => repo.DeleteAsync(authorId), Times.Once);
        }

    }
}