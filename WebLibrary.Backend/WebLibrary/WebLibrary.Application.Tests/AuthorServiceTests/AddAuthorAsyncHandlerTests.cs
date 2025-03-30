using AutoMapper;
using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Services;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorServiceTests
{
    public class AddAuthorAsyncHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthorService _authorService;

        public AddAuthorAsyncHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();

            _authorService = new AuthorService(
                _authorRepositoryMock.Object,
                Mock.Of<IBookRepository>(), // Не нужен для этого метода
                _mapperMock.Object);
        }

        [Fact]
        public async Task AddAuthorAsync_ValidAuthorDto_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var authorDto = new AuthorDto
            {
                AuthorId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Country = "USA"
            };

            var mappedAuthor = new Author
            {
                AuthorId = authorDto.AuthorId,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Country = authorDto.Country
            };

            _mapperMock.Setup(m => m.Map<Author>(authorDto)).Returns(mappedAuthor);
            _authorRepositoryMock.Setup(repo => repo.AddAsync(mappedAuthor)).Returns(Task.CompletedTask);

            // Act
            await _authorService.AddAuthorAsync(authorDto);

            // Assert
            _mapperMock.Verify(m => m.Map<Author>(authorDto), Times.Once);
            _authorRepositoryMock.Verify(repo => repo.AddAsync(mappedAuthor), Times.Once);
        }

    }
}
