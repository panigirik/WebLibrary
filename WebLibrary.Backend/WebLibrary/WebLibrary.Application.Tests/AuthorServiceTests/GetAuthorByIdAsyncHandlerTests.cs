using AutoMapper;
using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Services;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorServiceTests
{
    public class GetAuthorByIdAsyncHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthorService _authorService;

        public GetAuthorByIdAsyncHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();

            _authorService = new AuthorService(
                _authorRepositoryMock.Object,
                Mock.Of<IBookRepository>(), // Не нужен для тестирования этого метода
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetAuthorByIdAsync_AuthorExists_ReturnsMappedAuthorDto()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author { AuthorId = authorId, FirstName = "John", LastName = "Doe", Country = "USA" };
            var authorDto = new AuthorDto { AuthorId = authorId, FirstName = "John", LastName = "Doe", Country = "USA" };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
                .ReturnsAsync(author);
            _mapperMock.Setup(m => m.Map<AuthorDto?>(author))
                .Returns(authorDto);

            // Act
            var result = await _authorService.GetAuthorByIdAsync(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(authorId, result.AuthorId);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("USA", result.Country);
        }

        [Fact]
        public async Task GetAuthorByIdAsync_AuthorDoesNotExist_ReturnsNull()
        {
            // Arrange
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Author?)null);
            _mapperMock.Setup(m => m.Map<AuthorDto?>(null))
                .Returns((AuthorDto?)null);

            // Act
            var result = await _authorService.GetAuthorByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}
