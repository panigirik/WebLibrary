using AutoMapper;
using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Services;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorServiceTests;

    public class GetAllAuthorsAsyncHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthorService _authorService;

        public GetAllAuthorsAsyncHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();

            _authorService = new AuthorService(
                _authorRepositoryMock.Object,
                _bookRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAuthorsAsync_WhenAuthorsExist_ReturnsMappedAuthorDtos()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { AuthorId = Guid.NewGuid(), FirstName = "Author1", LastName = "LastName1", Country = "Country1" },
                new Author { AuthorId = Guid.NewGuid(), FirstName = "Author2", LastName = "LastName2", Country = "Country2" }
            };

            var authorDtos = new List<AuthorDto>
            {
                new AuthorDto { AuthorId = authors[0].AuthorId, FirstName = "Author1", LastName = "LastName1", Country = "Country1" },
                new AuthorDto { AuthorId = authors[1].AuthorId, FirstName = "Author2", LastName = "LastName2", Country = "Country2" }
            };

            _authorRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(authors);
            _mapperMock.Setup(m => m.Map<IEnumerable<AuthorDto>>(authors)).Returns(authorDtos);

            // Act
            var result = await _authorService.GetAllAuthorsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Author1", result.First().FirstName);
        }

        [Fact]
        public async Task GetAllAuthorsAsync_WhenNoAuthorsExist_ReturnsEmptyList()
        {
            // Arrange
            _authorRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(new List<Author>());
            _mapperMock.Setup(m => m.Map<IEnumerable<AuthorDto>>(It.IsAny<IEnumerable<Author>>())).Returns(new List<AuthorDto>());

            // Act
            var result = await _authorService.GetAllAuthorsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }