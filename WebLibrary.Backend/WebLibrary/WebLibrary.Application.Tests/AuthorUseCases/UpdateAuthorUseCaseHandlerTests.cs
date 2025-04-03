using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using AutoMapper;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorUseCases;

    public class UpdateAuthorUseCaseHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateAuthorUseCase _updateAuthorUseCase;

        public UpdateAuthorUseCaseHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();
            _updateAuthorUseCase = new UpdateAuthorUseCase(_authorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowNotFoundException_WhenAuthorDoesNotExist()
        {
            var authorDto = new AuthorDto { AuthorId = Guid.NewGuid() };  
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorDto.AuthorId))
                                 .ReturnsAsync((Author)null);
            
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _updateAuthorUseCase.ExecuteAsync(authorDto));
            Assert.Equal("Author with this id not found", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCallUpdateAsync_WhenAuthorExists()
        {
            var authorDto = new AuthorDto 
            { 
                AuthorId = Guid.NewGuid(), 
                FirstName = "Test", 
                LastName = "Author" 
            };

            var existingAuthor = new Author { 
                AuthorId = authorDto.AuthorId, 
                FirstName = "Old", 
                LastName = "Name" 
            };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorDto.AuthorId))
                                 .ReturnsAsync(existingAuthor);
            _mapperMock.Setup(mapper => mapper.Map<Author>(authorDto))
                       .Returns(new Author 
                       { 
                           AuthorId = authorDto.AuthorId, 
                           FirstName = authorDto.FirstName, 
                           LastName = authorDto.LastName 
                       });
            
            await _updateAuthorUseCase.ExecuteAsync(authorDto);
            
            _authorRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Author>(a => 
                a.FirstName == authorDto.FirstName && 
                a.LastName == authorDto.LastName)), Times.Once);  
        }

        [Fact]
        public async Task ExecuteAsync_ShouldMapDtoToEntity_WhenAuthorExists()
        {
            var authorDto = new AuthorDto 
            { 
                AuthorId = Guid.NewGuid(), 
                FirstName = "Test", 
                LastName = "Author" 
            };

            var existingAuthor = new Author 
            { 
                AuthorId = authorDto.AuthorId, 
                FirstName = "Old", 
                LastName = "Name" 
            };

            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorDto.AuthorId))
                                 .ReturnsAsync(existingAuthor);

            var mappedAuthor = new Author 
            { 
                AuthorId = authorDto.AuthorId, 
                FirstName = authorDto.FirstName, 
                LastName = authorDto.LastName 
            };
            _mapperMock.Setup(mapper => mapper.Map<Author>(authorDto)).Returns(mappedAuthor);
            
            await _updateAuthorUseCase.ExecuteAsync(authorDto);
            
            _mapperMock.Verify(mapper => mapper.Map<Author>(authorDto), Times.Once);
        }
    }

