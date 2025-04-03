using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using AutoMapper;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorUseCases;

    public class GetBooksAuthorUseCaseHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetBooksAuthorUseCase _getBooksAuthorUseCase;

        public GetBooksAuthorUseCaseHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapperMock = new Mock<IMapper>();
            _getBooksAuthorUseCase = new GetBooksAuthorUseCase(_bookRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnBooks_WhenAuthorHasBooks()
        {
            var authorId = Guid.NewGuid();
            var books = new List<Book>
            {
                new Book { BookId = Guid.NewGuid(), Title = "Book 1", AuthorId = authorId },
                new Book { BookId = Guid.NewGuid(), Title = "Book 2", AuthorId = authorId }
            };

            var bookDtos = new List<BookDto>
            {
                new BookDto { BookId = books[0].BookId, Title = books[0].Title },
                new BookDto { BookId = books[1].BookId, Title = books[1].Title }
            };

            _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthorIdAsync(authorId))
                .ReturnsAsync(books);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BookDto>>(books))
                .Returns(bookDtos);

            var result = await _getBooksAuthorUseCase.ExecuteAsync(authorId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Book 1", result.First().Title);
            Assert.Equal("Book 2", result.Skip(1).First().Title);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenAuthorHasNoBooks()
        {
            var authorId = Guid.NewGuid();
            _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthorIdAsync(authorId))
                .ReturnsAsync(new List<Book>());

           var result = await _getBooksAuthorUseCase.ExecuteAsync(authorId);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenRepositoryThrowsException()
        {
            var authorId = Guid.NewGuid();
            _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthorIdAsync(authorId))
                .ThrowsAsync(new Exception("Database error"));

            var exception = await Assert.ThrowsAsync<Exception>(() => _getBooksAuthorUseCase.ExecuteAsync(authorId));
            Assert.Equal("Database error", exception.Message);
        }
    }
    

