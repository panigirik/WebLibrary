using AutoMapper;
using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorUseCases
{
    public class DeleteAuthorUseCaseHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly DeleteAuthorUseCase _deleteAuthorUseCase;

        public DeleteAuthorUseCaseHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();
            _deleteAuthorUseCase = new DeleteAuthorUseCase(_authorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldDeleteAuthor_WhenAuthorExists()
        {
            var authorId = Guid.NewGuid();
            var authorDto = new AuthorDto { AuthorId = authorId, FirstName = "John", LastName = "Doe" };
            var authorEntity = new Author { AuthorId = authorId, FirstName = "John", LastName = "Doe" };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
                .ReturnsAsync(authorEntity);

            _mapperMock.Setup(m => m.Map<Author>(authorDto))
                .Returns(authorEntity);
            
            await _deleteAuthorUseCase.ExecuteAsync(authorDto);
            
            _authorRepositoryMock.Verify(repo => repo.DeleteAsync(authorEntity), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowNotFoundException_WhenAuthorDoesNotExist()
        {
            var authorId = Guid.NewGuid();
            var authorDto = new AuthorDto { AuthorId = authorId, FirstName = "John", LastName = "Doe" };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
                .ReturnsAsync((Author)null);
            
            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => _deleteAuthorUseCase.ExecuteAsync(authorDto)
            );

            Assert.Equal("Author with this id not found", exception.Message);
        }
    }
}
