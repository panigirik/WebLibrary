using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using AutoMapper;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorUseCases;

    public class GetAuthorByIdUseCaseHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAuthorByIdUseCase _getAuthorByIdUseCase;

        public GetAuthorByIdUseCaseHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();
            _getAuthorByIdUseCase = new GetAuthorByIdUseCase(_authorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnAuthorDto_WhenAuthorExists()
        {
            var authorId = Guid.NewGuid();
            var author = new Author
            {
                AuthorId = authorId,
                FirstName = "Test",
                LastName = "Author",
                DateOfBirth = new DateTime(1980, 1, 1),
                Country = "Test Country"
            };

            var authorDto = new AuthorDto
            {
                AuthorId = authorId,
                FirstName = "Test",
                LastName = "Author",
                DateOfBirth = new DateTime(1980, 1, 1),
                Country = "Test Country"
            };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
                .ReturnsAsync(author);
            _mapperMock.Setup(mapper => mapper.Map<AuthorDto>(author))
                .Returns(authorDto);

            var result = await _getAuthorByIdUseCase.ExecuteAsync(authorId);

            Assert.NotNull(result);
            Assert.Equal(authorDto.AuthorId, result.AuthorId);
            Assert.Equal(authorDto.FirstName, result.FirstName);
            Assert.Equal(authorDto.LastName, result.LastName);
            Assert.Equal(authorDto.Country, result.Country);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
           var authorId = Guid.NewGuid();
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
                .ReturnsAsync((Author)null);

            var result = await _getAuthorByIdUseCase.ExecuteAsync(authorId);

            Assert.Null(result);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldMapAuthorToAuthorDto_WhenAuthorExists()
        {
            var authorId = Guid.NewGuid();
            var author = new Author
            {
                AuthorId = authorId,
                FirstName = "Test",
                LastName = "Author",
                DateOfBirth = new DateTime(1980, 1, 1),
                Country = "Test Country"
            };

            var authorDto = new AuthorDto
            {
                AuthorId = authorId,
                FirstName = "Test",
                LastName = "Author",
                DateOfBirth = new DateTime(1980, 1, 1),
                Country = "Test Country"
            };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
                .ReturnsAsync(author);
            _mapperMock.Setup(mapper => mapper.Map<AuthorDto>(author))
                .Returns(authorDto);

            await _getAuthorByIdUseCase.ExecuteAsync(authorId);

           _mapperMock.Verify(mapper => mapper.Map<AuthorDto>(author), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenRepositoryThrowsException()
        {
            var authorId = Guid.NewGuid();
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
                .ThrowsAsync(new Exception("Database error"));

            var exception = await Assert.ThrowsAsync<Exception>(() => _getAuthorByIdUseCase.ExecuteAsync(authorId));
            Assert.Equal("Database error", exception.Message);
        }
    }

