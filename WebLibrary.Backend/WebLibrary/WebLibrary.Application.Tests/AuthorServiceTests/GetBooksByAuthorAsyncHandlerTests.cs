using AutoMapper;
using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Services;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorServiceTests
{
    public class GetBooksByAuthorAsyncHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthorService _authorService;

        public GetBooksByAuthorAsyncHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();

            _authorService = new AuthorService(
                Mock.Of<IAuthorRepository>(), // Не нужен для этого метода
                _bookRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetBooksByAuthorAsync_AuthorHasBooks_ReturnsMappedBooks()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var books = new List<Book>
            {
                new Book { BookId = Guid.NewGuid(), Title = "Book 1" },
                new Book { BookId = Guid.NewGuid(), Title = "Book 2" }
            };
            var bookDtos = new List<BookDto>
            {
                new BookDto { Title = "Book 1" },
                new BookDto { Title = "Book 2" }
            };

            _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthorIdAsync(authorId))
                .ReturnsAsync(books);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BookDto>>(books))
                .Returns(bookDtos);

            // Act
            var result = await _authorService.GetBooksByAuthorAsync(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                book => Assert.Equal("Book 1", book.Title),
                book => Assert.Equal("Book 2", book.Title));

            _bookRepositoryMock.Verify(repo => repo.GetBooksByAuthorIdAsync(authorId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<BookDto>>(books), Times.Once);
        }

        [Fact]
        public async Task GetBooksByAuthorAsync_AuthorHasNoBooks_ReturnsEmptyList()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthorIdAsync(authorId))
                .ReturnsAsync(new List<Book>());
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BookDto>>(It.IsAny<IEnumerable<Book>>()))
                .Returns(new List<BookDto>());

            // Act
            var result = await _authorService.GetBooksByAuthorAsync(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            _bookRepositoryMock.Verify(repo => repo.GetBooksByAuthorIdAsync(authorId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<BookDto>>(It.IsAny<IEnumerable<Book>>()), Times.Once);
        }
    }
}
